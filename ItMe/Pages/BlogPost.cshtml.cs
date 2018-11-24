using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItMe.Database;
using ItMe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Pages
{
    public class BlogPostModel : PageModel
    {
        public Person Person { get; set; }
        public BlogPost BlogPost { get; set; }

        private readonly ItMeDb db;

        public BlogPostModel(ItMeDb db)
        {
            this.db = db;
        }

        public async Task OnGetAsync(string slug)
        {
            var host = HttpContext.Request.Host.Host;
            Person = await db.Persons.Where(x => x.Host == host).Select(Mappers.MapPerson).SingleAsync();
            BlogPost = await db.BlogPosts.Where(x => x.PersonId == Person.Id && x.Slug == slug).Select(Mappers.MapBlogPost).SingleAsync();
        }
    }
}
