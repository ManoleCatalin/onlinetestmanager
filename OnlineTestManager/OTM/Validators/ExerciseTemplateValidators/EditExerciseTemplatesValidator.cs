using Constants;
using FluentValidation;
using OTM.ViewModels.ExerciseTemplate;

namespace OTM.Validators.ExerciseValidators
{
    public class EditExerciseTemplatesValidator : AbstractValidator<EditExerciseTemplatesViewModel>
    {
        public EditExerciseTemplatesValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage(string.Format(CoreConfigurationConstants.FieldEmptyMessage, "Description"))
                .MaximumLength(CoreConfigurationConstants.MaxLength)
                .WithMessage(string.Format(CoreConfigurationConstants.FieldMaximumLengthMessage, "Description", CoreConfigurationConstants.MaxLength));

        }
    }
}
