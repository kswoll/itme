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
    public class CvModel : PageModel
    {
        public Person Person { get; set; }
        public Cv Cv { get; set; }

        private readonly ItMeDb db;

        public CvModel(ItMeDb db)
        {
            this.db = db;
        }

        public async Task OnGet()
        {
            var host = HttpContext.Request.Host.Host;
            Person = await db.Persons.Where(x => x.Host == host).Select(Mappers.MapPerson).SingleAsync();

            Task<Cv> GetCv()
            {
                return db.Cvs.Where(x => x.PersonId == Person.Id).Select(Mappers.MapCv).SingleOrDefaultAsync();
            }

            Cv = await GetCv();
            if (Cv == null)
            {
                var dbCv = new DbCv
                {
                    PersonId = Person.Id
                };
                db.Cvs.Add(dbCv);
                await db.SaveChangesAsync();
                Cv = await GetCv();
            }
        }
    }
}