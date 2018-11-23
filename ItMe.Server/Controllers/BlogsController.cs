using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ItMe.Server.Database;
using ItMe.Server.Utils;
using ItMe.Shared.Models;
using ItMe.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Server.Controllers
{
    [Route("api/[controller]")]
    public class BlogsController : Controller
    {
        private static Regex slugPattern = new Regex(@"(\W)+");

        private readonly ItMeDb db;

        public BlogsController(ItMeDb db)
        {
            this.db = db;
        }

        [HttpGet("{id:int}")]
        public async Task<BlogPostModel> GetPost(int id)
        {
            return await db.BlogPosts.Where(x => x.Id == id).Select(Mappers.MapBlogPost).SingleAsync();
        }

        [HttpGet("{slug}")]
        public async Task<BlogPostModel> GetPost(string slug)
        {
            return await db.BlogPosts.Where(x => x.Slug == slug).Select(Mappers.MapBlogPost).SingleAsync();
        }

        [HttpGet]
        public async Task<BlogPostModel[]> GetPosts(int offset = 0, int limit = 25)
        {
            return await db.BlogPosts.Skip(offset).Take(limit).Select(Mappers.MapBlogPost).ToArrayAsync();
        }

        [HttpPost, Authorize]
        public async Task<BlogPostModel> CreatePost([FromBody]PutBlogPost post)
        {
            var dbBlogPost = new DbBlogPost
            {
                Title = post.Title,
                Body = post.Body,
                Excerpt = post.Excerpt,
                Slug = await GenerateSlug(post.Title),
                Created = DateTime.UtcNow,
                LastUpdated = DateTime.UtcNow,
                PersonId = User.GetId()
            };
            db.BlogPosts.Add(dbBlogPost);
            await db.SaveChangesAsync();

            return await GetPost(dbBlogPost.Id);
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