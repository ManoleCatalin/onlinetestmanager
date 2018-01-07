using FluentValidation;
using OTM.ViewModels.Group;
using Consts = Constants.CoreConfigurationConstants;

namespace OTM.Validators.GroupValidators
{
    public class EditGroupValidator : AbstractValidator<EditGroupViewModel>
    {
        public EditGroupValidator()
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
