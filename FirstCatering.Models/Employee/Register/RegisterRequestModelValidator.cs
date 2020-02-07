using FluentValidation;
using FirstCatering.Lib.Validation;

namespace FirstCatering.Models.Employee
{
    public class RegisterRequestModelValidator : Validator<RegisterRequestModel>
    {
        public RegisterRequestModelValidator()
        {
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("EmployeeId is required")
                .Matches(Regexes.EmployeeId).WithMessage("EmployeeId must be valid");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email must be a valid email address");
            RuleFor(x => x.MobileNumber)
                .NotEmpty().WithMessage("Mobile is required")
                .Matches(Regexes.Mobile).WithMessage("MobileNumber must be a valid UK mobile phone number");
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("CompanyId is required");
            RuleFor(x => x.PIN)
                .NotEmpty().WithMessage("PIN is required")
                .Matches(Regexes.PIN).WithMessage("Security PIN must be valid");
        }
    }
}