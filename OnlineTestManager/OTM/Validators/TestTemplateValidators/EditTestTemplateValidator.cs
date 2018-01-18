using Constants;
using Data.Core.Interfaces;
using FluentValidation;
using OTM.ViewModels.TestTemplates;

namespace OTM.Validators.TestTemplateValidators
{
    public class EditTestTemplateValidator : AbstractValidator<EditTestTemplatesViewModel>
    {
        private readonly ITestInstancesRepository _testInstancesRepository;
        public EditTestTemplateValidator(ITestInstancesRepository testInstancesRepository)
        {
            _testInstancesRepository = testInstancesRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(string.Format(CoreConfigurationConstants.FieldEmptyMessage, "Name"))
                .MaximumLength(CoreConfigurationConstants.MaxLength)
                .WithMessage(string.Format(CoreConfigurationConstants.FieldMaximumLengthMessage, "Name", CoreConfigurationConstants.MaxLength));

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(string.Format(CoreConfigurationConstants.FieldEmptyMessage, "Description"))
                .MaximumLength(CoreConfigurationConstants.MaxLength)
                .WithMessage(string.Format(CoreConfigurationConstants.FieldMaximumLengthMessage, "Description", CoreConfigurationConstants.MaxLength));
            RuleFor(x => x).Custom((x, context) =>
            {
                var testInstance = _testInstancesRepository.GetByIdAsync(x.Id).Result;
                if (testInstance == null)
                {
                    context.AddFailure("Id","Id must be valid");
                }
            });

        }
    }
}
