using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FirstCatering.Lib.Objects.Result;

namespace FirstCatering.Lib.AspNetCore.Results
{
    /// <summary>
    /// Default action result containing an <see cref="IResult"/>
    /// </summary>
    public class DefaultResult : IActionResult
    {
        /// <summary>
        /// The operation result
        /// </summary>
        private IResult Result { get; }

        /// <summary>
        /// Constructor to fill the action's result with an operation
        /// result
        /// </summary>
        /// <param name="result"><see cref="IResult"/> operation result</param>
        public DefaultResult(IResult result)
            => Result = result;

        /// <summary>
        /// Executes the result operation of the action method.
        /// This method is called by MVC to process the result of an
        /// action method
        /// </summary>
        /// <param name="context">Context in which result is executed</param>
        /// <returns><see cref="Task"/> asynchronous operation</returns>
        public async Task ExecuteResultAsync(ActionContext context)
        {
            object value = default;
            if (Result.IsError)
                value = Result.Message;
            else if ((Result.GetType() != typeof(Result)) && Result.GetType().GetGenericTypeDefinition() == typeof(DataResult<>))
                value = ((dynamic)Result).Data;

            var objectResult = new ObjectResult(value)
            {
                StatusCode = Result.IsSuccess ? StatusCodes.Status200OK : StatusCodes.Status422UnprocessableEntity
            };

            await objectResult.ExecuteResultAsync(context);
        }
    }
}