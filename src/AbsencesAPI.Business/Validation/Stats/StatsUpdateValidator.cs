using AbsencesAPI.Common.DTOS.Stats;
using FluentValidation;

namespace AbsencesAPI.Business.Validation.Stats;

public class StatsUpdateValidator : AbstractValidator<StatsUpdate>
{
	public StatsUpdateValidator()
	{
        RuleFor(r => r.Description).NotEmpty().MaximumLength(200);
        RuleFor(r => r.Value).GreaterThanOrEqualTo(0);
    }
}
