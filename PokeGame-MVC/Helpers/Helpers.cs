using System.Security.Claims;
using System.Security.Policy;

namespace PokeGame_MVC.Helpers
{
    public class Helpers
    {
        public static int GetCurrentUserId(ClaimsPrincipal User)
        {
            return int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
