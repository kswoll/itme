using System.Security.Claims;

namespace ItMe.Utils
{
    public static class ClaimsPrincipalExtensions
    {
		public static int GetId(this ClaimsPrincipal user)
		{
			return int.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier));
		}        
    }
}