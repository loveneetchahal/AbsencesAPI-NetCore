using AbsencesAPI.Common.DTOS.Absence;
using FluentValidation;

namespace AbsencesAPI.Business.Validation.Absence;

public class AbsenceCreateValidator : AbstractValidator<AbsenceCreate>
{
	public AbsenceCreateValidator()
	{
		RuleFor(r => r.Date).NotEmpty();
        RuleFor(r => r.Type).NotEmpty().MaximumLength(150);
    }
}
