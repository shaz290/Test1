using FluentValidation;

namespace NZWalks.API.Validators
{
    public class UpdateRegionRequestValidator:AbstractValidator<Models.DTO.UpdateWalkDifficultyRequest>
    {
        public UpdateRegionRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
    
}
