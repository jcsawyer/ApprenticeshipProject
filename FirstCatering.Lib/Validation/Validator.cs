using System.Threading.Tasks;
using FirstCatering.Lib.Objects.Result;
using FluentValidation;

namespace FirstCatering.Lib.Validation
{
    /// <summary>
    /// Model validator
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    public abstract class Validator<T> : AbstractValidator<T>
    {
        /// <summary>
        /// Validation message
        /// </summary>
        private string Message { get; set; }

        /// <summary>
        /// Runs validation rules against the given <paramref name="model"/>
        /// </summary>
        /// <param name="model">Model to validate</param>
        /// <returns><see cref="IResult"/> validation result</returns>
        public new IResult Validate(T model)
        {
            if (model == null)
                return Result.Error(Message ?? string.Empty);

            var result = base.Validate(model);
            if (result.IsValid)
                return Result.Success();

            return Result.Error(Message ?? result.ToString());
        }

        /// <summary>
        /// Asynchronously runs validation rules against the given <paramref name="model"/>
        /// </summary>
        /// <param name="model">Model to validate</param>
        /// <returns><see cref="IResult"/> validation result</returns>
        public Task<IResult> ValidateAsync(T model)
            => Task.FromResult(Validate(model));
    }
}