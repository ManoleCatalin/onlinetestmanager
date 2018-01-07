using Constants;
using FluentValidation;
using OTM.ViewModels.ExerciseTemplate;

namespace OTM.Validators.ExerciseValidators
{
    public class CreateExerciseTemplatesValidator : AbstractValidator<CreateExerciseTemplatesViewModel>
    {
        public CreateExerciseTemplatesValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage(string.Format(CoreConfigurationConstants.FieldEmptyMessage, "Description"))
                .MaximumLength(CoreConfigurationConstants.MaxLength)
                .WithMessage(string.Format(CoreConfigurationConstants.FieldMaximumLengthMessage, "Description", CoreConfigurationConstants.MaxLength));

        }
    }
}
