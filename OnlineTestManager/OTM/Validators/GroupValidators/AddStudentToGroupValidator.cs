using Data.Core.Interfaces;
using FluentValidation;
using OTM.ViewModels.Group;
using Consts = Constants.CoreConfigurationConstants;

namespace OTM.Validators.GroupValidators
{
    public class AddStudentToGroupValidator : AbstractValidator<AddStudentToGroupViewModel>
    {
        private readonly IUsersRepository _usersRepository;
        public AddStudentToGroupValidator(IUsersRepository users)
        {
            _usersRepository = users;
            RuleFor(x => x.StudentName)
                .NotEmpty().WithMessage(string.Format(Consts.FieldEmptyMessage, "Student Name"))
                .MaximumLength(Consts.MaxLength)
                .WithMessage(string.Format(Consts.FieldMaximumLengthMessage, "Student Name", Consts.MaxLength))
                .Custom((x, context) =>
                {
                    var student = _usersRepository.GetStudentsByNamePrefixAsync(x).Result;
                    if (student.Count == 0)
                    {
                        context.AddFailure("StudentName", "Student name is not valid");
                    }
                });
        }
    }
}
