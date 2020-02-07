using System;
using System.Threading.Tasks;
using FirstCatering.Lib.Extensions;
using FirstCatering.Lib.Logging;
using FirstCatering.Models.Employee;
using FirstCatering.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstCatering.Api.Controllers
{
    /// <summary>
    /// Employee Api controller
    /// Route: /Employee/
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : BaseController
    {
        private readonly ILogger logger;
        private readonly IEmployeeService employeeService;

        /// <summary>
        /// Instantiates an <see cref="EmployeeController"/> with the specified
        /// <paramref name="logger"/> and <paramref name="employeeService"/>
        /// </summary>
        /// <param name="logger"> Logger</param>
        /// <param name="employeeService"> <see cref="IEmployeeService"/> employee service</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EmployeeController(
            ILogger logger,
            IEmployeeService employeeService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
        }

        /// <summary>
        /// Identifies whether an Employee Id exists in the database. When it does,
        /// it returns the API id for the employee to be used in a login request.
        /// When the employee does not exist in the system, a HTTP422 error is returned
        /// and a register request should be made.
        /// </summary>
        /// <param name="request"><see cref="IdentifyRequestModel" /> identification request</param>
        /// <returns>Employee's id for login or an error indicating registration required</returns>
        [HttpPost("Identify")]
        [AllowAnonymous]
        public async Task<IActionResult> IdentifyAsync(IdentifyRequestModel request)
            => Result(await employeeService.IdentifyAsync(request));

        /// <summary>
        /// Registers an employee for the given <see cref="RegisterRequestModel" /> 
        /// returning the new API id for the employee that can be used to make a login
        /// request.
        /// </summary>
        /// <param name="request"><see cref="RegisterRequestModel" /> registration request</param>
        /// <returns>Employee's id for login or an error with message</returns>
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync(RegisterRequestModel request)
            => Result(await employeeService.RegisterAsync(request));

        /// <summary>
        /// Authenticates an employee from the given <see cref="LoginRequestModel" /> 
        /// returning a <see cref="LoginResponseModel" /> response on success. The token 
        /// returned must be used in the Authorization header using the Bearer schema 
        /// whenever accessing restricted endpoints.
        /// </summary>
        /// <param name="request"><see cref="LoginRequestModel" /> login request</param>
        /// <returns><see cref="LoginResponseModel" /> response or error with message</returns>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginRequestModel request)
            => Result(await employeeService.LoginAsync(request));


        /// <summary>
        /// Retrieves the balance for the currently authenticated employee.
        /// </summary>
        /// <returns>Employee's balance</returns>
        [HttpGet("Balance")]
        public async Task<IActionResult> BalanceAsync()
            => Result(await employeeService.BalanceAsync(User.GetUserId()));
    }
}