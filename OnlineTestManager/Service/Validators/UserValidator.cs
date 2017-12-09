using Data.Core.Domain;
using FluentValidation;

namespace Service.Validators
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().Length(5, 30);
            RuleFor(x => x.LastName).NotEmpty().Length(5, 30);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.PasswordHash).NotEmpty();
        }
    }
}
