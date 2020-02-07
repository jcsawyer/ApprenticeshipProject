using System;
using System.Threading.Tasks;
using FirstCatering.Data;
using FirstCatering.Domain;
using FirstCatering.Lib.Logging;
using FirstCatering.Lib.Objects.Result;
using FirstCatering.Models.Transaction;

namespace FirstCatering.Services.Transaction
{
    /// <summary>
    /// Transaction service
    /// </summary>
    public class TransactionService : ITransactionService
    {
        /// <summary>
        /// <see cref="ILogger"/> logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// EF database context
        /// </summary>
        private readonly FirstCateringDbContext db;

        /// <summary>
        /// Initialises a new <see cref="TransactionService"/> with the given
        /// <paramref name="logger"/> and <paramref name="db"/>
        /// </summary>
        /// <param name="logger"><see cref="ILogger"/> logger</param>
        /// <param name="db"><see cref="FirstCateringDbContext"/> EF database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        public TransactionService(
            ILogger logger,
            FirstCateringDbContext db)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.db = db ?? throw new ArgumentNullException(nameof(db));
        }

        /// <summary>
        /// Removes specified amount from <paramref name="request"/> from the given
        /// <paramref name="employeeId"/>
        /// </summary>
        /// <param name="request"><see cref="SpendRequestModel"/> spend request</param>
        /// <param name="employeeId">Id of employee to remove balance from</param>
        /// <returns>The employee's new balance or error</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IDataResult<decimal>> Spend(
            SpendRequestModel request,
            long employeeId)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            
            var validation = await new SpendRequestModelValidator().ValidateAsync(request);
            if (!validation.IsSuccess)
                return DataResult<decimal>.Error(validation.Message);

            var employee = await db.Set<EmployeeEntity>().FindAsync(employeeId);
            if (employee == null)
                throw new InvalidOperationException("Cannot spend null employee's balance");
            
            if (employee.Balance < request.Amount)
                return DataResult<decimal>.Error("Not enough balance");
            
            var transaction = TransactionEntityFactory.Create(employee, -request.Amount);
            employee.ChangeBalance(-request.Amount);

            await db.AddAsync(transaction);
            db.Update(employee);
            await db.SaveChangesAsync();

            return DataResult<decimal>.Success(employee.Balance);
        }

        /// <summary>
        /// Adds specified amount from <paramref name="request"/> to the given
        /// <paramref name="employeeId"/>
        /// </summary>
        /// <param name="request"><see cref="TopUpRequestModel"/> top up request</param>
        /// <param name="employeeId">Id of employee to add amount to balance</param>
        /// <returns>The employee's new balance or error</returns>
        public async Task<IDataResult<decimal>> TopUp(
            TopUpRequestModel request,
            long employeeId)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var validation = await new TopUpRequestModelValidator().ValidateAsync(request);
            if (!validation.IsSuccess)
                return DataResult<decimal>.Error(validation.Message);

            var employee = await db.Set<EmployeeEntity>().FindAsync(employeeId);
            if (employee == null)
                throw new InvalidOperationException("Cannot top up null employee's balance");
            
            var transaction = TransactionEntityFactory.Create(employee, request.Amount);
            employee.ChangeBalance(request.Amount);

            await db.AddAsync(transaction);
            db.Update(employee);
            await db.SaveChangesAsync();

            return DataResult<decimal>.Success(employee.Balance);
        }
    }
}