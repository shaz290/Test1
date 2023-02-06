using FluentValidation;

namespace NZWalks.API.Validators
{
    public class AddWalkDifficultyValidators:AbstractValidator<Models.DTO.AddWalkDifficultyRequest>
    {
        public AddWalkDifficultyValidators()
        {
            RuleFor(x =>x.Code).NotEmpty();
        }
    }
}
