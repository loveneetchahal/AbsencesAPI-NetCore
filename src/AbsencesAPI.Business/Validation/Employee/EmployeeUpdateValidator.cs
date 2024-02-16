using AbsencesAPI.Common.DTOS.Employee;
using FluentValidation;

namespace AbsencesAPI.Business.Validation.Employee;

public class EmployeeUpdateValidator : AbstractValidator<EmployeeUpdate>
{
	public EmployeeUpdateValidator()
	{
		RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
        RuleFor(r => r.Department).NotEmpty().MaximumLength(100);
    }
}
