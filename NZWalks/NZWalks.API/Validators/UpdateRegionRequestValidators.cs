using FluentValidation;

namespace NZWalks.API.Validators
{
    public class UpdateRegionRequestValidators: AbstractValidator<Models.DTO.UpdateRegionRequest>
    {
        public UpdateRegionRequestValidators()
        {

            RuleFor(x => x.Code).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Area).GreaterThan(0);
            RuleFor(x => x.Population).GreaterThanOrEqualTo(0);
        }
    }
}
