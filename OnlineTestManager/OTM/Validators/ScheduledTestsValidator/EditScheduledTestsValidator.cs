using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using OTM.ViewModels.ScheduledTest;

namespace OTM.Validators.ScheduledTestsValidator
{
    public class EditScheduledTestsValidator : AbstractValidator<EditScheduledTestViewModel>
    {
        public EditScheduledTestsValidator()
        {
            RuleFor(x => x.Duration).GreaterThanOrEqualTo(1)
               .WithMessage("Duration must be greater or equal to 1");
        }
    }
}
