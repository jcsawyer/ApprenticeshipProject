using System;

namespace FirstCatering.Lib.Security.Jwt
{
    /// <summary>
    /// Json web token configuration definition
    /// </summary>
    public interface IJsonWebTokenSettings
    {
        /// <summary>
        /// Valid token audience
        /// </summary>
        string Audience { get; }

        /// <summary>
        /// Length of time token is valid
        /// </summary>
        TimeSpan Expires { get; }

        /// <summary>
        /// Token issuer
        /// </summary>
        string Issuer { get; }

        /// <summary>
        /// Token key
        /// </summary>
        string Key { get; }
    }
}