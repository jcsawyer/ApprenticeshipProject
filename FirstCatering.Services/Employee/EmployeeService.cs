using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FirstCatering.Data;
using FirstCatering.Domain;
using FirstCatering.Lib.Extensions;
using FirstCatering.Lib.Logging;
using FirstCatering.Lib.Objects.Result;
using FirstCatering.Lib.Security.Hashing;
using FirstCatering.Lib.Security.Jwt;
using FirstCatering.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace FirstCatering.Services
{
    /// <summary>
    /// Employee service
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// <see cref="ILogger"/> Logger
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// EF database context
        /// </summary>
        private readonly FirstCateringDbContext db;

        /// <summary>
        /// Json web token service
        /// </summary>
        private readonly IJsonWebToken jsonWebToken;

        /// <summary>
        /// Hash service
        /// </summary>
        private readonly IHash hash;

        /// <summary>
        /// Initialises a new <see cref="EmployeeService"/> with the given <paramref name="logger"/>,
        /// <paramref name="db"/>, <paramref name="jsonWebToken"/> and <paramref name="hash"/>
        /// </summary>
        /// <param name="logger"><see cref="Ilogger"/> Logger</param>
        /// <param name="db"><see cref="FirstCateringDbContext"/> EF database context</param>
        /// <param name="jsonWebToken"><see cref="IJsonWebToken"/> jwt service</param>
        /// <param name="hash"><see cref="IHash"/> hash service</param>
        /// <exception cref="ArgumentNullException"></exception>
        public EmployeeService(
            ILogger logger,
            FirstCateringDbContext db,
            IJsonWebToken jsonWebToken,
            IHash hash)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.db = db ?? throw new ArgumentNullException(nameof(db));
            this.jsonWebToken = jsonWebToken ?? throw new ArgumentNullException(nameof(jsonWebToken));
            this.hash = hash ?? throw new ArgumentNullException(nameof(hash));
        }

        /// <summary>
        /// Identifies whether an Employee Id exists in the database.
        /// </summary>
        /// <param name="request"><see cref="IdentifyRequestModel"/> identification request</param>
        /// <returns><see cref="IDataResult{long}"/> employee id or error message</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IDataResult<long>> IdentifyAsync(IdentifyRequestModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var validation = await new IdentifyRequestModelValidator().ValidateAsync(request);
            if (!validation.IsSuccess)
                return DataResult<long>.Error(validation.Message);

            if (!await db.Set<CompanyEntity>().AnyAsync(x => x.Id == request.CompanyId))
                return DataResult<long>.Error("Company not found");

            var employee = await db.Set<EmployeeEntity>()
                .FirstOrDefaultAsync(x => x.CompanyId == request.CompanyId && x.EmployeeId == request.EmployeeId);

            if (employee == null)
                return DataResult<long>.Error("Employee not found");
            return DataResult<long>.Success(employee.Id);
        }

        /// <summary>
        /// Registers an employee for the given <paramref name="request"/>
        /// </summary>
        /// <param name="request"><see cref="RegisterRequestModel"/> register request</param>
        /// <returns><see cref="IDataResult{long}"/> new employee id or error message</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IDataResult<long>> RegisterAsync(RegisterRequestModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var validation = await new RegisterRequestModelValidator().ValidateAsync(request);
            if (!validation.IsSuccess)
                return DataResult<long>.Error(validation.Message);

            
            if (!await db.Set<CompanyEntity>().AnyAsync(x => x.Id == request.CompanyId))
                return DataResult<long>.Error("Company not found");

            if (await db.Set<EmployeeEntity>().AnyAsync(x => x.CompanyId == request.CompanyId && x.EmployeeId == request.EmployeeId))
                return DataResult<long>.Error("Employee already registered");


            request.PIN = hash.Create(request.PIN, request.EmployeeId);
            var employee = EmployeeEntityFactory.Create(request);
            await db.AddAsync(employee);
            await db.SaveChangesAsync();

            return DataResult<long>.Success(employee.Id);
        }

        /// <summary>
        /// Authenticates an employee from the given <paramref name="request"/>
        /// </summary>
        /// <param name="request"><see cref="LoginRequestModel"/> login request</param>
        /// <returns><see cref="IDataResult{LoginResponseModel}"/> login response or error</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<IDataResult<LoginResponseModel>> LoginAsync(LoginRequestModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var validation = await new LoginRequestModelValidator().ValidateAsync(request);
            if (!validation.IsSuccess)
                return DataResult<LoginResponseModel>.Error(validation.Message);

            var employee = await db.Set<EmployeeEntity>().FirstOrDefaultAsync(x => x.Id == request.Id);
            if (employee == null)
                return DataResult<LoginResponseModel>.Error("Invalid login");

            request.PIN = hash.Create(request.PIN, employee.EmployeeId);

            if (!request.PIN.Equals(employee.PIN))
            {
                DateTime fiveMinutes = DateTime.Now - TimeSpan.FromMinutes(5);
                int loginFailures = await db.Set<LoginEntity>()
                    .CountAsync(x => !x.Success && x.Timestamp >= fiveMinutes);
                if (loginFailures > 0) 
                {
                    var lastLoginFailure = await db.Set<LoginEntity>().OrderByDescending(x => x.Timestamp).FirstOrDefaultAsync(x => !x.Success);
                    if (lastLoginFailure.DidLock && lastLoginFailure.Timestamp >= fiveMinutes)
                        return DataResult<LoginResponseModel>.Error("Employee locked");
                }
                var failedLogin = LoginEntityFactory.Create(request.Id, request.KioskId, false, loginFailures >= 2);
                await db.AddAsync(failedLogin);
                await db.SaveChangesAsync();
                return DataResult<LoginResponseModel>.Error("Invalid login");
            }

            var response = await GenerateToken(request);

            var login = LoginEntityFactory.Create(request.Id, request.KioskId, true, false);
            await db.AddAsync(login);
            await db.SaveChangesAsync();

            return DataResult<LoginResponseModel>.Success(response);
        }

        /// <summary>
        /// Generates an authentication token from the given <paramref name="request"/>
        /// </summary>
        /// <param name="request"><see cref="LoginRequestModel"/> login request</param>
        /// <returns><see cref="LoginResponseModel"/> login response</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        private async Task<LoginResponseModel> GenerateToken(LoginRequestModel request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var employee = await db.Set<EmployeeEntity>().FirstOrDefaultAsync(x => x.Id == request.Id && x.PIN == request.PIN);
            if (employee == null)
                throw new InvalidOperationException("Cannot generate token for null employee");
            
            var claims = new List<Claim>();
            claims.AddSub(employee.Id.ToString());
            claims.AddJti();
            claims.AddNameIdentifier(employee.Id.ToString());
            claims.AddName(employee.Name);
            string token = jsonWebToken.Encode(claims);

            var response = new LoginResponseModel(
                token,
                employee.Name,
                employee.Balance);
            return response;
        }

        /// <summary>
        /// Retrieves the balance for the specified <paramref name="employeeId"/>
        /// </summary>
        /// <param name="employeeId">Id of employee's balance to retrieve</param>
        /// <returns>Employee's balance or error</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IDataResult<decimal>> BalanceAsync(long employeeId)
        {
            if (employeeId <= 0)
                throw new ArgumentOutOfRangeException(nameof(employeeId));
            
            var employee = await db.Set<EmployeeEntity>().FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee == null)
                throw new InvalidOperationException("Cannot find balance for null employee");
            
            return DataResult<decimal>.Success(employee.Balance);
        }
    }
}