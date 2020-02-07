using System.Collections.Generic;
using System.Security.Claims;

namespace FirstCatering.Lib.Security.Jwt
{
    /// <summary>
    /// Json web token definition
    /// </summary>
    public interface IJsonWebToken
    {
        /// <summary>
        /// Decodes a collection of encoded Jwt claims
        /// </summary>
        /// <param name="token">Jwt token</param>
        /// <returns>Collection of claims</returns>
         Dictionary<string, object> Decode(string token);

         /// <summary>
         /// Encodes a collection of Jwt claims into a token
         /// </summary>
         /// <param name="claims">Jwt claims</param>
         /// <returns>Jwt token</returns>
        string Encode(IList<Claim> claims);
    }
}