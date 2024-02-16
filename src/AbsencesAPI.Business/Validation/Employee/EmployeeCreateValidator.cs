using AbsencesAPI.Common.DTOS.Employee;
using FluentValidation;

namespace AbsencesAPI.Business.Validation.Employee;

public class EmployeeCreateValidator : AbstractValidator<EmployeeCreate>
{
	public EmployeeCreateValidator()
	{
        RuleFor(r => r.Name).NotEmpty().MaximumLength(100);
        RuleFor(r => r.Department).NotEmpty().MaximumLength(100);
    }
}
