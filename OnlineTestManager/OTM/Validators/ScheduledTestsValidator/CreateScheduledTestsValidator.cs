using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using OTM.ViewModels.ScheduledTest;

namespace OTM.Validators.ScheduledTestsValidator
{
    public class CreateScheduledTestsValidator : AbstractValidator<CreateScheduledTestViewModel>
    {
        public CreateScheduledTestsValidator()
        {
            RuleFor(x => x.Duration).GreaterThanOrEqualTo(1)
               .WithMessage("Duration must be greater or equal to 1");
            RuleFor(x => x.StartDateTime).GreaterThan(DateTime.Now).WithMessage("Test must start with at least one second later.");
        }
    }
}
