using Microsoft.AspNetCore.Mvc;
using FirstCatering.Lib.AspNetCore.Results;
using FirstCatering.Lib.Objects.Result;

namespace FirstCatering.Api.Controllers
{
    /// <summary>
    /// FirstCatering API base controller
    /// </summary>
    public abstract class BaseController : ControllerBase
    {
        /// <summary>
        /// Creates a default action result for the given <see cref="IResult"/>
        /// </summary>
        /// <param name="result"><see cref="IResult"/> result</param>
        /// <returns><see cref="DefaultResult"/> Result</returns>
        protected static DefaultResult Result(IResult result)
            => new DefaultResult(result);
    }
}