using FluentValidation;
using FirstCatering.Lib.Validation;

namespace FirstCatering.Models.Employee
{
    public class IdentifyRequestModelValidator : Validator<IdentifyRequestModel>
    {
        public IdentifyRequestModelValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty().WithMessage("CompanyId is required");
            RuleFor(x => x.EmployeeId)
                .NotEmpty().WithMessage("EmployeeId is required")
                .Matches(Regexes.EmployeeId).WithMessage("EmployeeId must be valid");
        }
    }
}