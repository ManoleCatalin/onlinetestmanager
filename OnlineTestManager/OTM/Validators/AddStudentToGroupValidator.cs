using FluentValidation;
using OTM.ViewModels.Group;
using Consts = Constants.CoreConfigurationConstants;

namespace OTM.Validators
{
    public class AddStudentToGroupValidator : AbstractValidator<AddStudentToGroupViewModel>
    {
        public AddStudentToGroupValidator()
        {
            RuleFor(x => x.StudentName)
                .NotEmpty().WithMessage(string.Format(Consts.FieldEmptyMessage, "Student Name"))
                .MaximumLength(Consts.MaxLength)
                .WithMessage(string.Format(Consts.FieldMaximumLengthMessage, "Student Name", Consts.MaxLength));
        }
    }
}
