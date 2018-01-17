using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Core.Interfaces;
using FluentValidation;
using OTM.ViewModels.ScheduledTest;

namespace OTM.Validators.ScheduledTestsValidator
{
    public class EditScheduledTestsValidator : AbstractValidator<EditScheduledTestViewModel>
    {
        private readonly ITestInstancesRepository _testInstancesRepository;
        public EditScheduledTestsValidator(ITestInstancesRepository testInstancesRepository)
        {
            _testInstancesRepository = testInstancesRepository;
            RuleFor(x => x.Duration).GreaterThanOrEqualTo(1)
               .WithMessage("Duration must be greater or equal to 1");
            RuleFor(x => x).Custom((x, context) =>
            {
                var testInstance = _testInstancesRepository.GetByIdAsync(x.Id).Result;
                if (testInstance == null)
                {
                    context.AddFailure("Id","Id must be valid");
                }
                if (x.StartDateTime < DateTime.Now)
                {
                    context.AddFailure("StartDate","StartDate must valid");
                }
            });
        }
    }
}
