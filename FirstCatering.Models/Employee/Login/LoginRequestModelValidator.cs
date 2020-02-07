using FluentValidation;
using FirstCatering.Lib.Validation;

namespace FirstCatering.Models.Employee
{
    public class LoginRequestModelValidator : Validator<LoginRequestModel>
    {
        public LoginRequestModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
            RuleFor(x => x.PIN)
                .NotEmpty().WithMessage("PIN is required")
                .Matches(Regexes.PIN).WithMessage("PIN must be valid");
            RuleFor(x => x.KioskId)
                .NotEmpty().WithMessage("KioskId is required");
        }
    }
}