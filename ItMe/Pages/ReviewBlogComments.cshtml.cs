using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ReviewBlogCommentsModel : PageModel
    {
        public List<BlogPostComment> Comments { get; set; }

        private readonly ItMeDb db;

        public ReviewBlogCommentsModel(ItMeDb db)
        {
            this.db = db;
        }

        public async Task OnGetAsync()
        {
            Comments = await db.BlogPostComments.Where(x => x.Status == BlogPostCommentStatus.Pending).Select(Mappers.MapBlogPostComment).ToListAsync();


        }

        public async Task<IActionResult> OnPostApproveAsync(int id)
        {
            var dbBlogPost = await db.BlogPostComments.Where(x => x.Id == id).Select(x => x.BlogPost).SingleAsync();
            var dbBlogPostComment = await db.BlogPostComments.SingleAsync(x => x.Id == id);
            dbBlogPostComment.Status = BlogPostCommentStatus.Approved;
            await db.SaveChangesAsync();

            return RedirectToPage("BlogPost", new { slug = dbBlogPost.Slug });
        }
    }
}