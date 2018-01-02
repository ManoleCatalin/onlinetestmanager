using Constants;
using FluentValidation;
using OTM.ViewModels.AnswerTemplate;

namespace OTM.Validators.AnswersValidators
{
    public class EditAnswerTemplatesValidator : AbstractValidator<EditAnswerTemplatesViewModel>
    {
        public EditAnswerTemplatesValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage(string.Format(CoreConfigurationConstants.FieldEmptyMessage, "Description"))
                .MaximumLength(CoreConfigurationConstants.MaxLength)
                .WithMessage(string.Format(CoreConfigurationConstants.FieldMaximumLengthMessage, "Description", CoreConfigurationConstants.MaxLength));
        }
    }
}
