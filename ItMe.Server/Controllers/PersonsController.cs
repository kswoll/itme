using System.Linq;
using System.Threading.Tasks;
using ItMe.Server.Database;
using ItMe.Server.Utils;
using ItMe.Shared.Models;
using ItMe.Shared.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Server.Controllers
{
    [Route("api/[controller]")]
    public class PersonsController : Controller
    {
        private readonly ItMeDb db;

        public PersonsController(ItMeDb db)
        {
            this.db = db;
        }

        [HttpGet("me")]
        public async Task<PersonModel> Get()
        {
            var host = HttpContext.Request.Host;
            return await db.Persons.Where(x => x.Host == host.Host).Select(Mappers.MapPerson).SingleAsync();
        }
    }
}