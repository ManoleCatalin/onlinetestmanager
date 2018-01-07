using Constants;
using FluentValidation;
using OTM.ViewModels.TestTemplates;

namespace OTM.Validators.TestTemplateValidators
{
    public class CreateTestTemplateValidator : AbstractValidator<CreateTestTemplatesViewModel>
    {
        public CreateTestTemplateValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(string.Format(CoreConfigurationConstants.FieldEmptyMessage, "Name"))
                .MaximumLength(CoreConfigurationConstants.MaxLength)
                .WithMessage(string.Format(CoreConfigurationConstants.FieldMaximumLengthMessage, "Name", CoreConfigurationConstants.MaxLength));

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(string.Format(CoreConfigurationConstants.FieldEmptyMessage, "Description"))
                .MaximumLength(CoreConfigurationConstants.MaxLength)
                .WithMessage(string.Format(CoreConfigurationConstants.FieldMaximumLengthMessage, "Description", CoreConfigurationConstants.MaxLength));
        }
    }
}
