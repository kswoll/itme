using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ItMe.Database;
using ItMe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Pages
{
    [Authorize]
    public class EditBlogPostModel : PageModel
    {
        public Person Person { get; set; }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Body { get; set; }

        [BindProperty]
        public string Excerpt { get; set; }

        private readonly ItMeDb db;

        private static readonly Regex slugPattern = new Regex(@"(\W)+");

        public EditBlogPostModel(ItMeDb db)
        {
            this.db = db;
        }

        public async Task OnGetAsync(string slug)
        {
            var host = HttpContext.Request.Host.Host;
            Person = await db.Persons.Where(x => x.Host == host).Select(Mappers.MapPerson).SingleAsync();
            var dbBlogPost = await db.BlogPosts.Where(x => x.PersonId == Person.Id && x.Slug == slug).SingleOrDefaultAsync();
            if (dbBlogPost != null)
            {
                Title = dbBlogPost.Title;
                Body = dbBlogPost.Body;
                Excerpt = dbBlogPost.Excerpt;
            }
        }

        public async Task<IActionResult> OnPostAsync(string slug)
        {
            var host = HttpContext.Request.Host.Host;
            Person = await db.Persons.Where(x => x.Host == host).Select(Mappers.MapPerson).SingleAsync();
            var dbBlogPost = await db.BlogPosts.Where(x => x.PersonId == Person.Id && x.Slug == slug).SingleOrDefaultAsync();
            if (dbBlogPost == null)
            {
                dbBlogPost = new DbBlogPost();
                db.BlogPosts.Add(dbBlogPost);
                dbBlogPost.Slug = await GenerateSlug(Title);
                dbBlogPost.Created = DateTime.UtcNow;
                dbBlogPost.PersonId = Person.Id;
            }
            dbBlogPost.Title = Title;
            dbBlogPost.Body = Body;
            dbBlogPost.Excerpt = Excerpt;
            dbBlogPost.LastUpdated = DateTime.UtcNow;
            await db.SaveChangesAsync();

            return RedirectToPage("BlogPost", new { dbBlogPost.Slug });
        }

        private async Task<string> GenerateSlug(string title)
        {
            string slug = title.ToLower();
            slug = slugPattern.Replace(slug, "_");

            int? counter = null;
            while (true)
            {
                var isUnique = !await db.BlogPosts.AnyAsync(x => x.Slug == slug);
                if (isUnique)
                {
                    return slug;
                }
                if (counter == null)
                {
                    counter = 2;
                }
                else
                {
                    counter++;
                }
                slug = $"{slug}_{counter}";
            }
        }
    }
}