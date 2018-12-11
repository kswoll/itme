using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SimpleEmail.Model;
using ItMe.Database;
using ItMe.Models;
using ItMe.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Pages
{
    public class BlogPostModel : PageModel
    {
        public Person Person { get; set; }
        public BlogPost BlogPost { get; set; }
        public List<BlogPostComment> Comments { get; set; }

        [BindProperty]
        public string CommentName { get; set; }

        [BindProperty]
        public string CommentBody { get; set; }

        [BindProperty(Name = "g-recaptcha-response")]
        public string GoogleRecaptchaResponse { get; set; }

        private readonly ItMeDb db;
        private readonly IRazorViewToStringRenderer renderer;

        public BlogPostModel(ItMeDb db, IRazorViewToStringRenderer renderer)
        {
            this.db = db;
            this.renderer = renderer;
        }

        public async Task OnGetAsync(string slug)
        {
            var host = HttpContext.Request.Host.Host;
            Person = await db.Persons.Where(x => x.Host == host).Select(Mappers.MapPerson).SingleAsync();
            BlogPost = await db.BlogPosts.Where(x => x.PersonId == Person.Id && x.Slug == slug).Select(Mappers.MapBlogPost).SingleAsync();
            Comments = await db.BlogPostComments.Where(x => x.BlogPostId == BlogPost.Id && x.Status == BlogPostCommentStatus.Approved).Select(Mappers.MapBlogPostComment).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(string slug)
        {
            if (!await RecaptchaUtils.ValidateResponse(GoogleRecaptchaResponse))
            {
                return Unauthorized();
            }

            var host = HttpContext.Request.Host.Host;
            Person = await db.Persons.Where(x => x.Host == host).Select(Mappers.MapPerson).SingleAsync();
            var post = await db.BlogPosts.Where(x => x.PersonId == Person.Id && x.Slug == slug).SingleAsync();

            var dbComment = new DbBlogPostComment
            {
                BlogPost = post,
                Created = DateTime.UtcNow,
                Body = CommentBody,
                AuthorName = CommentName,
                Status = BlogPostCommentStatus.Pending
            };

            db.BlogPostComments.Add(dbComment);
            await db.SaveChangesAsync();

            var text = await renderer.RenderViewToStringAsync("CommentReviewEmail", new CommentReviewEmailModel
            {
                Name = CommentName,
                Body = CommentBody
            });

            using (var client = Aws.CreateEmailClient())
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = "me@kirkwoll.com",
                    Destination = new Destination
                    {
                        ToAddresses = new List<string>
                        {
                            "kirk.woll@gmail.com"
                        }
                    },
                    Message = new Message
                    {
                        Subject = new Content(CommentName),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = text
                            }
                        }
                    }
                };

                await client.SendEmailAsync(sendRequest);
            }

            return RedirectToPage("BlogPost", new { slug = post.Slug });
        }
    }
}
