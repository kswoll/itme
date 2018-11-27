using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ItMe.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ItMe.Pages
{
    public class FavIconModel : PageModel
    {
        private readonly ItMeDb db;
        private readonly IHostingEnvironment hostingEnvironment;

        public FavIconModel(ItMeDb db, IHostingEnvironment hostingEnvironment)
        {
            this.db = db;
            this.hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> OnGet()
        {
            var host = HttpContext.Request.Host.Host;
            var dbPerson = db.Persons.SingleOrDefault(x => x.Host == host);
            var s = Environment.CurrentDirectory;
            if (dbPerson == null)
            {
                var path = hostingEnvironment.WebRootPath;
                var favIcon = Path.Combine("images", "favicon.ico");
                return this.File(favIcon, "image/x-icon");
            }

            return this.Content("test");
        }
    }
}