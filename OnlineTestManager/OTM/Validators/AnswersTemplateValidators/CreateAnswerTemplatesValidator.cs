using Constants;
using FluentValidation;
using OTM.ViewModels.AnswerTemplate;

namespace OTM.Validators.AnswersValidators
{
    public class CreateAnswerTemplatesValidator : AbstractValidator<CreateAnswerTemplatesViewModel>
    {
        public CreateAnswerTemplatesValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage(string.Format(CoreConfigurationConstants.FieldEmptyMessage, "Description"))
                .MaximumLength(CoreConfigurationConstants.MaxLength)
                .WithMessage(string.Format(CoreConfigurationConstants.FieldMaximumLengthMessage, "Description", CoreConfigurationConstants.MaxLength));
        }
    }
}
