using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstCatering.Domain
{
    /// <summary>
    /// A transaction entity
    /// </summary>
    public class TransactionEntity
    {
        /// <summary>
        /// Unique identifer
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Date and time the transaction was made
        /// </summary>
        public DateTime Timestamp { get; set; } = DateTime.Now;

        /// <summary>
        /// Id of the employee that made the transaction
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// Instance of the employee that made the transaction
        /// </summary>
        public virtual EmployeeEntity Employee { get; set; }

        /// <summary>
        /// The amount transacted
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// The employee's balance prior to the transaction
        /// </summary>
        public decimal PreviousBalance { get; set; }

        /// <summary>
        /// The employee's balance after the transaction
        /// </summary>
        [NotMapped]
        public decimal NewBalance => PreviousBalance + Amount;

        /// <summary>
        /// Initialises a <see cref="TransactionEntity"/> with the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">Transaction id</param>
        public TransactionEntity(long id)
            => Id = id;

        /// <summary>
        /// Initialises a <see cref="TransactionEntity"/> with the specified <paramref name="id"/>,
        /// <paramref name="employeeId"/>, <paramref name="amount"/> and <paramref name="previousBalance"/>
        /// </summary>
        /// <param name="id">Transaction id</param>
        /// <param name="employeeId">Employee id</param>
        /// <param name="amount">Amount transacted</param>
        /// <param name="previousBalance">The employee balance prior to transaction</param>
        public TransactionEntity(
            long id,
            long employeeId,
            decimal amount,
            decimal previousBalance)
        {
            Id = id;
            EmployeeId = employeeId;
            Amount = amount;
            PreviousBalance = previousBalance;
        }
    }
}