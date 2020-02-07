using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FirstCatering.Lib.Extensions
{
    /// <summary>
    /// <see cref="ICollection{Claim}"/> extension helpers
    /// </summary>
    public static class ClaimsExtensions
    {
        /// <summary>
        /// Adds a Jwt Id claim
        /// </summary>
        public static void AddJti(this ICollection<Claim> claims)
            => claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        /// <summary>
        /// Adds a Jwt subject claim
        /// </summary>
        /// <param name="sub">Subject</param>
        public static void AddSub(this ICollection<Claim> claims, string sub)
            => claims.Add(new Claim(JwtRegisteredClaimNames.Sub, sub));

        /// <summary>
        /// Adds a Jwt name identifier claim
        /// </summary>
        /// <param name="nameIdentifier">Name identifier</param>
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
            => claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));

        /// <summary>
        /// Adds a Jwt name claim
        /// </summary>
        /// <param name="name">Name</param>
        public static void AddName(this ICollection<Claim> claims, string name)
            => claims.Add(new Claim(ClaimTypes.Name, name));
    }
}