using System.Linq;
using Data.Core.Interfaces;
using FluentValidation;
using OTM.UserContext;
using OTM.ViewModels.Group;
using Consts = Constants.CoreConfigurationConstants;

namespace OTM.Validators.GroupValidators
{
    public class AddStudentToGroupValidator : AbstractValidator<AddStudentToGroupViewModel>
    {
        private readonly IUsersRepository _usersRepository;
        
        public AddStudentToGroupValidator(IUsersRepository users, IUserContext user, IGroupsRepository groups)
        {
            _usersRepository = users;
            RuleFor(x => x.StudentName)
                .NotEmpty().WithMessage(string.Format(Consts.FieldEmptyMessage, "Student Name"))
                .MaximumLength(Consts.MaxLength)
                .WithMessage(string.Format(Consts.FieldMaximumLengthMessage, "Student Name", Consts.MaxLength));
               
            RuleFor(x=>x).Custom((x, context) =>
            {
                var student = _usersRepository.GetStudentsByNamePrefixAsync(x.StudentName).Result;
                if (student.Count == 0)
                {
                    context.AddFailure("StudentName", "Student name is not valid");
                    return;
                }
                var group = groups.GetByIdAsync(x.GroupId).Result;
                if (null == group)
                {
                    context.AddFailure("", "Invalid group id");
                    return;
                }
                foreach (var userGroup in group.UserGroups)
                {
                    if (userGroup.User.UserName.Equals(x.StudentName))
                    {
                        context.AddFailure("StudentName", "This user is already in this group");
                        return;
                    }
                }
            });
            RuleFor(x => x.GroupId)
                .NotEmpty().WithMessage(string.Format(Consts.FieldEmptyMessage, "Group Id"))
                .Custom((x, context) =>
                {
                    var groupsList = groups.GetAllAsync().Result.Where(a => a.Id == x).ToList();

                    if (groupsList.Count == 0)
                    {
                        context.AddFailure("Group Id", "Group Id is not valid");
                    }
                    else
                    {

                        if (user.GetLogedInUserId() != groupsList[0].UserId)
                        {
                            context.AddFailure("Group Id", "Unauthorized");
                        }
                    }
                });
           
        }
    }
}
