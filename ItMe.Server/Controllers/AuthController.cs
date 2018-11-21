using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ItMe.Server.Database;
using ItMe.Server.Utils;
using ItMe.Shared.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ItMe.Server.Controllers
{
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
		private readonly ItMeDb db;

	    public AuthController(ItMeDb db)
	    {
		    this.db = db;
	    }

	    [HttpPost, AllowAnonymous]
		public async Task<IActionResult> Login([FromBody]PutLogin login)
		{
			var personLogin = await db.PersonLogins
			    .Include(x => x.Person)
			    .SingleAsync(x => x.Person.Email == login.Email);

			if (personLogin == null || !PasswordUtils.IsValid(login.Password, personLogin.PasswordHash))
				return new UnauthorizedResult();

			return Json(GenerateToken(personLogin));
		}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]PostPerson register)
        {
            var dbPerson = new DbPerson
            {
                Email = register.Email,
                Name = register.Name,
                Host = register.Host,
                Port = register.Port
            };
            var dbPersonLogin = new DbPersonLogin
            {
                Person = dbPerson,
                PasswordHash = PasswordUtils.HashPassword(register.Password)
            };
            db.Persons.Add(dbPerson);
            db.PersonLogins.Add(dbPersonLogin);
            await db.SaveChangesAsync();

            return Json(GenerateToken(dbPersonLogin));
        }

		private string GenerateToken(DbPersonLogin personLogin)
	    {
			var person = personLogin.Person;

            var featureClaims = db.Features
                .Where(x => x.PersonId == personLogin.PersonId)
                .Select(x => new Claim($"feature:{x.Type.ToString()}", "enabled"))
                .ToArray();

	        var baseClaims = new[]
	        {
	            new Claim(ClaimTypes.Name, person.Name),
	            new Claim(ClaimTypes.Email, personLogin.Person.Email),
	            new Claim(ClaimTypes.NameIdentifier, personLogin.Person.Id.ToString()),
	            new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
	            new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddYears(1)).ToUnixTimeSeconds().ToString())
	        };

			var claims = baseClaims.Concat(featureClaims).ToArray();

	        var token = new JwtSecurityToken(
	            new JwtHeader(new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("LaddlerLaddlerLaddler")), SecurityAlgorithms.HmacSha256)),
	            new JwtPayload(claims));

	        return new JwtSecurityTokenHandler().WriteToken(token);
	    }
    }
}