using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Constants;
using Data.Core.Domain;
using Data.Core.Interfaces;
using FluentValidation;
using OTM.UserContext;
using OTM.ViewModels.AnswerTemplate;

namespace OTM.Validators.AnswersValidators
{
    public class CreateAnswerTemplatesValidator : AbstractValidator<CreateAnswerTemplatesViewModel>
    {
        private readonly IExercisesRepository _exerciseRepository;

        public CreateAnswerTemplatesValidator(ITestsRepository tests, IExercisesRepository exercises, IUserContext user)
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage(string.Format(CoreConfigurationConstants.FieldEmptyMessage, "Description"))
                .MaximumLength(CoreConfigurationConstants.MaxLength)
                .WithMessage(string.Format(CoreConfigurationConstants.FieldMaximumLengthMessage, "Description", CoreConfigurationConstants.MaxLength));

            _exerciseRepository = exercises;

            List<Test> TestId = new List<Test>();

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
                    TestId = testsList;
                });
           
            RuleFor(x => x.ExerciseTemplateId)
                .NotEmpty().WithMessage(string.Format(CoreConfigurationConstants.FieldEmptyMessage, "Exercise Template Id"))
                .Custom((x, context) =>
                {
                    var exercisesList = exercises.GetAllAsync().Result.Where(a => a.Id == x).ToList();

                    if (exercisesList.Count == 0)
                    {
                        context.AddFailure("Exercise Template Id", "Exercise Template Idis not valid");
                    }
                    else
                    {

                        if (TestId[0].Id != exercisesList[0].TestId)
                        {
                            context.AddFailure("Exercise Template Id", "Unauthorized");
                        }
                    }
                });


        }
    }
}
