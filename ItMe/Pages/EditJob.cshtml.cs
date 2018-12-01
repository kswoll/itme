using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItMe.Database;
using ItMe.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Pages
{
    public class EditJobModel : PageModel
    {
        [BindProperty]
        public string Company { get; set; }

        [BindProperty]
        public string Start { get; set; }

        [BindProperty]
        public string End { get; set; }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Description { get; set; }

        private readonly ItMeDb db;

        public EditJobModel(ItMeDb db)
        {
            this.db = db;
        }

        public async Task OnGet()
        {
            var dbJob = await db.Jobs.Where(x => x.Cv.PersonId == User.GetId()).SingleAsync();
            Company = dbJob.Company;
//            Start = dbJob.
        }
    }
}