namespace FirstCatering.Domain
{
    /// <summary>
    /// Login entity factory
    /// </summary>
    public static class LoginEntityFactory
    {
        /// <summary>
        /// Creates a <see cref="LoginEntity"/> from the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">Login id</param>
        /// <returns><see cref="LoginEntity"/> login</returns>
        public static LoginEntity Create(long id)
            => new LoginEntity(id, default, default, default, default);

        /// <summary>
        /// Creates a <see cref="LoginEntity"/> from the specified <paramref name="employeeId"/>
        /// and <paramref name="kioskId"/>
        /// </summary>
        /// <param name="employeeId">Employee id</param>
        /// <param name="kioskId">Kiosk id</param>
        /// /// <param name="success">Whether login was a success or not</param>
        /// <param name="didLock">Whether failure locked the employee account for 5 minutes</param>
        /// <returns><see cref="LoginEntity"/> login</returns>
        public static LoginEntity Create(
            long employeeId,
            string kioskId,
            bool success,
            bool didLock)
        => new LoginEntity(
            default,
            employeeId,
            kioskId,
            success,
            didLock);
    }
}