using System;
using System.Security.Cryptography;
using System.Text;

namespace FirstCatering.Lib.Security.Hashing
{
    /// <summary>
    /// Hashing implementation
    /// </summary>
    public class Hash : IHash
    {
        /// <summary>
        /// Number of hash iterations
        /// </summary>
        public int Iterations { get; }

        /// <summary>
        /// Size of hash
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Initialises a new <see cref="Hash"/> using the specified <paramref name="iterations"/> 
        /// and <paramref name="size"/>
        /// </summary>
        /// <param name="iterations">Number of hash iterations</param>
        /// <param name="size">Size of hash</param>
        public Hash(int iterations, int size)
        {
            Iterations = iterations;
            Size = size;
        }

        /// <summary>
        /// Creates a SHA512 hash of <paramref name="value"/> using
        /// the given <paramref name="salt"/>
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <param name="salt">Hash salt</param>
        /// <returns>Hashed value</returns>
        public string Create(string value, string salt)
        {
            using (var algorithm = new Rfc2898DeriveBytes(value, Encoding.UTF8.GetBytes(salt), Iterations, HashAlgorithmName.SHA512))
                return Convert.ToBase64String(algorithm.GetBytes(Size));
        }
    }
}