using System.Linq;
using Constants;
using Data.Core.Interfaces;
using FluentValidation;
using OTM.UserContext;
using OTM.ViewModels.ExerciseTemplate;

namespace OTM.Validators.ExerciseValidators
{
    public class CreateExerciseTemplatesValidator : AbstractValidator<CreateExerciseTemplatesViewModel>
    {
        public CreateExerciseTemplatesValidator(ITestsRepository tests, IUserContext user)
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage(string.Format(CoreConfigurationConstants.FieldEmptyMessage, "Description"))
                .MaximumLength(CoreConfigurationConstants.MaxLength)
                .WithMessage(string.Format(CoreConfigurationConstants.FieldMaximumLengthMessage, "Description", CoreConfigurationConstants.MaxLength));

            RuleFor(x => x.TestTemplateId)
                .NotEmpty().WithMessage(string.Format(CoreConfigurationConstants.FieldEmptyMessage, "Test Template Id"))
                .Custom((x, context) =>
                {
                    var testsList = tests.GetAllAsync().Result.Where(a => a.Id == x).ToList();

                    if (testsList.Count == 0)
                    {
                        context.AddFailure("Test Template Id", "Test Template Id is not valid");
                    }
                    else
                    {

                        if (user.GetLogedInUserId() != testsList[0].UserId)
                        {
                            context.AddFailure("Test Template Id", "Unauthorized");
                        }
                    }
  
                });

        }
    }
}
