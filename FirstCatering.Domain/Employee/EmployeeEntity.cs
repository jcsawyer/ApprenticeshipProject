using System;
using System.Collections.Generic;

namespace FirstCatering.Domain
{
    /// <summary>
    /// An employee entity
    /// </summary>
    public class EmployeeEntity
    {
        /// <summary>
        /// Unique identifer
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Date and time employee registered
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// Employee id given by their assocaited company
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// Employee name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Employee email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Employee mobile phone number
        /// </summary>
        public string MobileNumber { get; set; }

        /// <summary>
        /// Unique identifier of the company the employee belongs to
        /// </summary>
        public long CompanyId { get; set; }

        /// <summary>
        /// Instance of the company the employee belongs to
        /// </summary>
        public virtual CompanyEntity Company { get; set; }

        /// <summary>
        /// 4 digit security pin
        /// </summary>
        public string PIN { get; set; }

        /// <summary>
        /// Employee's credit balance
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// Collection of employee's login history
        /// </summary>
        public virtual ICollection<LoginEntity> Logins { get; set; }

        /// <summary>
        /// Collection of employee's transaction history
        /// </summary>
        public virtual ICollection<TransactionEntity> Transactions { get; set; }

        /// <summary>
        /// Initialises a <see cref="EmployeeEntity"/> with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">Employee id</param>
        public EmployeeEntity(long id)
            => Id = id;

        /// <summary>
        /// Initialises a <see cref="EmployeeEntity"/> with the specified <paramref name="id"/>,
        /// <paramref name="employeeId"/>, <paramref name="name"/>, <paramref name="email"/>,
        /// <paramref name="mobileNumber"/>, <paramref name="companyId"/>, <paramref name="pin"/>
        /// and <paramref name="balance"/>
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <param name="employeeId">Company issued employee id</param>
        /// <param name="name">Employee name</param>
        /// <param name="email">Employee email address</param>
        /// <param name="mobileNumber">Employee mobile phone number</param>
        /// <param name="companyId">Employee company id</param>
        /// <param name="pin">Employee security pin</param>
        /// <param name="balance">Employee credit balance</param>
        public EmployeeEntity(
            long id,
            string employeeId,
            string name,
            string email,
            string mobileNumber,
            long companyId,
            string pin,
            decimal balance)
        {
            Id = id;
            EmployeeId = employeeId;
            Name = name;
            Email = email;
            MobileNumber = mobileNumber;
            CompanyId = companyId;
            PIN = pin;
            Balance = balance;
        }

        /// <summary>
        /// Changes the employee's balance by the specified <paramref name="amount"/>
        /// </summary>
        /// <param name="amount">Amount to change balance</param>
        public void ChangeBalance(decimal amount)
            => Balance += amount;
    }
}