using AbsencesAPI.Common.DTOS.Management;
using FluentValidation;

namespace AbsencesAPI.Business.Validation.Management;

public class ManagementUpdateValidator : AbstractValidator<ManagementUpdate>
{
	public ManagementUpdateValidator()
	{
		RuleFor(r => r.Manager).NotEmpty().MaximumLength(100);
    }
}
