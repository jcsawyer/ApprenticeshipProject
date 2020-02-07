using System;
using System.Threading.Tasks;
using FirstCatering.Lib.Extensions;
using FirstCatering.Lib.Logging;
using FirstCatering.Models.Transaction;
using FirstCatering.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstCatering.Api.Controllers
{
    /// <summary>
    /// Transaction Api controller
    /// Route: /Transaction/
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TransactionController : BaseController
    {
        private readonly ILogger logger;
        private readonly ITransactionService transactionService;

        /// <summary>
        /// Instantiates a <see cref="TransactionController"/> with the specified
        /// <paramref name="logger"/> and <paramref name="transactionService"/>
        /// </summary>
        /// <param name="logger"> Logger</param>
        /// <param name="transactionService"> transaction service</param>
        /// <exception cref="ArgumentNullException"></exception>
        public TransactionController(
            ILogger logger,
            ITransactionService transactionService)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
        }

        /// <summary>
        /// Removes specified amount from the currently logged in employee's 
        /// credit balance.
        /// </summary>
        /// <param name="request"><see cref="SpendRequestModel" /> spend request</param>
        /// <returns>The employee's new balance or error with message</returns>
        [HttpPost("Spend")]
        public async Task<IActionResult> Spend(SpendRequestModel request)
            => Result(await transactionService.Spend(request, User.GetUserId()));

        /// <summary>
        /// Adds the specified amount to the currently logged in employee's 
        /// credit balance.
        /// </summary>
        /// <param name="request"><see cref="TopUpRequestModel" /> top up request</param>
        /// <returns>The employee's new balance or error with message</returns>
        [HttpPost("TopUp")]
        public async Task<IActionResult> TopUp(TopUpRequestModel request)
            => Result(await transactionService.TopUp(request, User.GetUserId()));
    }
}