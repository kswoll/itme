using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using ItMe.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Pages
{
    public class SitemapModel : PageModel
    {
        private readonly ItMeDb db;

        public SitemapModel(ItMeDb db)
        {
            this.db = db;
        }

        public async Task<IActionResult> OnGet()
        {
            var urlset = new XElement("urlset");
            var document = new XDocument(urlset);

            var host = HttpContext.Request.Host.Host;
            var person = await db.Persons.Where(x => x.Host == host).Select(Mappers.MapPerson).SingleAsync();

            AddUrl(urlset, "", await db.BlogPosts.Where(x => x.PersonId == person.Id).Select(x => x.Created).OrderByDescending(x => x).FirstOrDefaultAsync());

            foreach (var blogPost in db.BlogPosts)
            {
                AddUrl(urlset, $"blog/{blogPost.Slug}", blogPost.Created);
            }

            return Content(document.ToString());
        }

        private void AddUrl(XElement urlset, string url, DateTime lastModified)
        {
            var urlElement = new XElement("url");
            var locElement = new XElement("loc") { Value = $"https://kirkwoll.com/{url}" };
            var lastmodElement = new XElement("lastmod") { Value = lastModified.ToString("yyyy-MM-dd") };
            urlElement.Add(locElement);
            urlElement.Add(lastmodElement);
            urlset.Add(urlElement);
        }
    }
}