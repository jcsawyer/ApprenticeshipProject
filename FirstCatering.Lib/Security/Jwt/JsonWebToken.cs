using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FirstCatering.Lib.Security.Jwt
{
    /// <summary>
    /// Json web token
    /// </summary>
    public class JsonWebToken : IJsonWebToken
    {
        /// <summary>
        /// Token configuration
        /// </summary>
        private IJsonWebTokenSettings Settings { get; }

        /// <summary>
        /// Token signing credentials
        /// </summary>
        private SigningCredentials Credentials { get; }

        /// <summary>
        /// Initialises a new <see cref="JsonWebToken"/> with the specified
        /// <paramref name="settings"/>
        /// </summary>
        /// <param name="settings"><see cref="IJsonWebTokenSettings"/> token configuration</param>
        public JsonWebToken(IJsonWebTokenSettings settings)
        {
            Settings = settings;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Settings.Key));
            Credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        }

        /// <summary>
        /// Decodes a collection of encoded Jwt claims
        /// </summary>
        /// <param name="token">Jwt token</param>
        /// <returns>Collection of claims</returns>
        public Dictionary<string, object> Decode(string token)
            => new JwtSecurityTokenHandler().ReadJwtToken(token).Payload;

        /// <summary>
        /// Encodes a collection of Jwt claims into a token
        /// </summary>
        /// <param name="claims">Jwt claims</param>
        /// <returns>Jwt token</returns>
        public string Encode(IList<Claim> claims)
        {
            if (claims == default)
                claims = new List<Claim>();

            var token = new JwtSecurityToken(
                Settings.Issuer,
                Settings.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.Add(Settings.Expires),
                Credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}