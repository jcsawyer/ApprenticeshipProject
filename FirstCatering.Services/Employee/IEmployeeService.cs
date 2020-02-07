using System.Threading.Tasks;
using FirstCatering.Lib.Objects.Result;
using FirstCatering.Models.Employee;

namespace FirstCatering.Services
{
    /// <summary>
    /// Employee service definition
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Identifies whether an Employee Id exists in the database.
        /// </summary>
        /// <param name="request"><see cref="IdentifyRequestModel"/> identification request</param>
        /// <returns><see cref="IDataResult{long}"/> employee id or error message</returns>
        Task<IDataResult<long>> IdentifyAsync(IdentifyRequestModel request);

        /// <summary>
        /// Registers an employee for the given <paramref name="request"/>
        /// </summary>
        /// <param name="request"><see cref="RegisterRequestModel"/> register request</param>
        /// <returns><see cref="IDataResult{long}"/> new employee id or error message</returns>
        Task<IDataResult<long>> RegisterAsync(RegisterRequestModel request);

        /// <summary>
        /// Authenticates an employee from the given <paramref name="request"/>
        /// </summary>
        /// <param name="request"><see cref="LoginRequestModel"/> login request</param>
        /// <returns><see cref="IDataResult{LoginResponseModel}"/> login response or error</returns>
        Task<IDataResult<LoginResponseModel>> LoginAsync(LoginRequestModel request);

        /// <summary>
        /// Retrieves the balance for the specified <paramref name="employeeId"/>
        /// </summary>
        /// <param name="employeeId">Id of employee's balance to retrieve</param>
        /// <returns>Employee's balance or error</returns>
        Task<IDataResult<decimal>> BalanceAsync(long employeeId);
    }
}