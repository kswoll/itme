using System.Security.Claims;

namespace ItMe.Server.Utils
{
    public static class ClaimsPrincipalExtensions
    {
		public static int GetId(this ClaimsPrincipal user)
		{
			return int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
		}        
    }
}