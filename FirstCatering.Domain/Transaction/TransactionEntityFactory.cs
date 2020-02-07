namespace FirstCatering.Domain
{
    /// <summary>
    /// Transaction entity factory
    /// </summary>
    public static class TransactionEntityFactory
    {
        /// <summary>
        /// Creates a <see cref="TransactionEntity"/> from the specified <paramref name="id"/>
        /// </summary>
        /// <param name="id">Transaction id</param>
        /// <returns><see cref="TransactionEntity"/> transaction</returns>
        public static TransactionEntity Create(long id)
            => new TransactionEntity(id, default, default, default);

        /// <summary>
        /// Creates a <see cref="TransactionEntity"/> from the specified <paramref name="employee"/>
        /// and <paramref name="amount"/>
        /// </summary>
        /// <param name="employee"><see cref="EmployeeEntity"/> employee</param>
        /// <param name="amount">Amount to transact</param>
        /// <returns><see cref="TransactionEntity"/> transaction</returns>
        public static TransactionEntity Create(
                EmployeeEntity employee,
                decimal amount)
            => new TransactionEntity(default, employee.Id, amount, employee.Balance);
    }
}