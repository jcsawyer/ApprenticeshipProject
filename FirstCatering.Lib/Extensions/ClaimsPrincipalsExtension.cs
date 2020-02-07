using System.Security.Claims;

namespace FirstCatering.Lib.Extensions
{
    /// <summary>
    /// <see cref="ClaimsPrincipal"/> extension helpers
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Gets the user id from the given <paramref name="claimsPrincipal"/>
        /// </summary>
        /// <returns>User id or 0</returns>
        public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
            => long.TryParse(claimsPrincipal?.FindFirstValue(ClaimTypes.NameIdentifier), out var value) ? value : 0;
    }
}