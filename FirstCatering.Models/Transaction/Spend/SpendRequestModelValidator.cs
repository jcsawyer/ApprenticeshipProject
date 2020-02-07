using FluentValidation;
using FirstCatering.Lib.Validation;

namespace FirstCatering.Models.Transaction
{
    public class SpendRequestModelValidator : Validator<SpendRequestModel>
    {
        public SpendRequestModelValidator()
        {
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Cannot spend zero or negative amount")
                .ScalePrecision(2, 8).WithMessage("Amount is maximum 8 digits and maximum 2 decimal places");
        }
    }
}