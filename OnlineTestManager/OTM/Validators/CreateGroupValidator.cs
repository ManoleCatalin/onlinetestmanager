using FluentValidation;
using OTM.Models.GroupViewModels;

using Consts = Constants.CoreConfigurationConstants;

namespace OTM.Validators
{
    public class CreateGroupValidator : AbstractValidator<CreateGroupViewModel>
    {
        public CreateGroupValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(string.Format(Consts.FieldEmptyMessage, "Name"))
                .MaximumLength(Consts.MaxLength)
                .WithMessage(string.Format(Consts.FieldMaximumLengthMessage, "Name", Consts.MaxLength));

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(string.Format(Consts.FieldEmptyMessage, "Description"))
                .MaximumLength(Consts.MaxLength)
                .WithMessage(string.Format(Consts.FieldMaximumLengthMessage, "Description", Consts.MaxLength));
        }
    }
}
