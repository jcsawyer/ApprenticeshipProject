using System;

namespace FirstCatering.Domain
{
    /// <summary>
    /// A login entity
    /// </summary>
    public class LoginEntity
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Date and time of login
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// Id of employee that logged in
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// Instance of employee that logged in
        /// </summary>
        public virtual EmployeeEntity Employee { get; set; }

        /// <summary>
        /// Kiosk identifier the employee logged in from
        /// </summary>
        public string KioskId { get; set; }

        /// <summary>
        /// Whether the login was a success or not
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Whether the login attempt locked the employee account for 5 minutes
        /// </summary>
        public bool DidLock { get; set; }

        /// <summary>
        /// Initialises a <see cref="LoginEntity"/> with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">Login id</param>
        public LoginEntity(long id)
            => Id = id;

        /// <summary>
        /// Initialises a <see cref="LoginEntity"/> with the specified <paramref name="id"/>, 
        /// <paramref name="employeeId"/> and <paramref name="kioskId"/>
        /// </summary>
        /// <param name="id">Login id</param>
        /// <param name="employeeId">Employee id</param>
        /// <param name="kioskId">Kiosk id</param>
        /// <param name="success">Whether login was a success or not</param>
        /// <param name="didLock">Whether failure locked the employee account for 5 minutes</param>
        public LoginEntity(
            long id,
            long employeeId,
            string kioskId,
            bool success,
            bool didLock)
        {
            Id = id;
            EmployeeId = employeeId;
            KioskId = kioskId;
            Success = success;
            DidLock = didLock;
        }
    }
}