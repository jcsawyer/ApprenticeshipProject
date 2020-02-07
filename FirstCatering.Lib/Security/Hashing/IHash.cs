namespace FirstCatering.Lib.Security.Hashing
{
    public interface IHash
    {
        /// <summary>
        /// Creates a hash for the specified value using the specified salt
        /// </summary>
        /// <param name="value">Value to hash</param>
        /// <param name="salt">Salt to alter hash result. Must be minimum 8-bytes</param>
        /// <returns>Hashed value</returns>
        string Create(string value, string salt);
    }
}