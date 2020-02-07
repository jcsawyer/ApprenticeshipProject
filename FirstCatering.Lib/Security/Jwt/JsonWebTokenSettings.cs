using System;

namespace FirstCatering.Lib.Security.Jwt
{
    /// <summary>
    /// Json web token settings
    /// </summary>
    public class JsonWebTokenSettings : IJsonWebTokenSettings
    {
        /// <summary>
        /// Valid token audience
        /// </summary>
        public string Audience { get; }

        /// <summary>
        /// Length of time token is valid
        /// </summary>
        public TimeSpan Expires { get; }

        /// <summary>
        /// Token issuer
        /// </summary>
        public string Issuer { get; }

        /// <summary>
        /// Token key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Initialises a <see cref="JsonWebTokenSettings"/> with the given
        /// <paramref name="key"/> and expiration <paramref name="expires"/>
        /// </summary>
        /// <param name="key">Token key</param>
        /// <param name="expires">Length of time token is valid</param>
        public JsonWebTokenSettings(string key, TimeSpan expires)
        {
            Key = key;
            Expires = expires;
        }

        /// <summary>
        /// Initialises a <see cref="JsonWebTokenSettings"/> with the given
        /// <paramref name="key"/>, expiration <paramref name="expires"/>,
        /// <paramref name="audience"/> and <paramref name="issuer"/>
        /// </summary>
        /// <param name="key">Token key</param>
        /// <param name="expires">Length of time token is valid</param>
        /// <param name="audience">Valid token audience</param>
        /// <param name="issuer">Token issuer</param>
        public JsonWebTokenSettings(string key, TimeSpan expires, string audience, string issuer) : this(key, expires)
        {
            Audience = audience;
            Issuer = issuer;
        }
    }
}