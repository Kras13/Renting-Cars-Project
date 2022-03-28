using System.Security.Claims;

namespace CarRentingSystem.Infrastructure
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
