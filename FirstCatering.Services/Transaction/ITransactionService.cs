using System.Threading.Tasks;
using FirstCatering.Lib.Objects.Result;
using FirstCatering.Models.Transaction;

namespace FirstCatering.Services
{
    /// <summary>
    /// Transaction service definition
    /// </summary>
    public interface ITransactionService
    {
        /// <summary>
        /// Removes specified amount from <paramref name="request"/> from the given
        /// <paramref name="employeeId"/>
        /// </summary>
        /// <param name="request"><see cref="SpendRequestModel"/> spend request</param>
        /// <param name="employeeId">Id of employee to remove balance from</param>
        /// <returns>The employee's new balance or error</returns>
         Task<IDataResult<decimal>> Spend(SpendRequestModel request, long employeeId);

        /// <summary>
        /// Adds specified amount from <paramref name="request"/> to the given
        /// <paramref name="employeeId"/>
        /// </summary>
        /// <param name="request"><see cref="TopUpRequestModel"/> top up request</param>
        /// <param name="employeeId">Id of employee to add amount to balance</param>
        /// <returns>The employee's new balance or error</returns>
         Task<IDataResult<decimal>> TopUp(TopUpRequestModel request, long employeeId);
    }
}