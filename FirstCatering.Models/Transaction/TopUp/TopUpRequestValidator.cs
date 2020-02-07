using FluentValidation;
using FirstCatering.Lib.Validation;

namespace FirstCatering.Models.Transaction
{
    public class TopUpRequestModelValidator : Validator<TopUpRequestModel>
    {
        public TopUpRequestModelValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Cannot top up zero or negative amount")
                .ScalePrecision(2, 8).WithMessage("Amount is maximum 8 digits and maximum 2 decimal places");
        }
    }
}