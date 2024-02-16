using AbsencesAPI.Common.DTOS.Management;
using FluentValidation;

namespace AbsencesAPI.Business.Validation.Management;

public class ManagementCreateValidator : AbstractValidator<ManagementCreate>
{
	public ManagementCreateValidator()
	{
		RuleFor(r => r.Manager).NotEmpty().MaximumLength(100);
    }
}
