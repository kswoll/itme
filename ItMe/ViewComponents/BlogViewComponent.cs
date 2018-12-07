using System.Linq;
using System.Threading.Tasks;
using ItMe.Database;
using ItMe.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItMe.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly ItMeDb db;

        public BlogViewComponent(ItMeDb db)
        {
            this.db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var host = HttpContext.Request.Host.Host;
            var posts = await db.BlogPosts.Where(x => x.Person.Host == host).OrderByDescending(x => x.Created).Select(Mappers.MapBlogPost).ToArrayAsync();
            var person = await db.Persons.Where(x => x.Host == host).Select(Mappers.MapPerson).SingleAsync();

            return View(new BlogViewComponentModel
            {
                Person = person,
                BlogPosts = posts
            });
        }
    }
}