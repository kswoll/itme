using System.Linq;
using System.Threading.Tasks;
using ItMe.Database;
using ItMe.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ItMe.Utils
{
    public class GlobalsManager
    {
        public Person Person { get; set; }

        private readonly ItMeDb db;
        private readonly IHttpContextAccessor httpContextAccessor;

        public GlobalsManager(ItMeDb db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task InitializeAsync()
        {
            Person = await db.Persons
                .Where(x => x.Host == httpContextAccessor.HttpContext.Request.Host.Host)
                .Select(Mappers.MapPerson)
                .SingleAsync();
        }
    }
}