using Data.Core.Interfaces;
using FluentValidation;
using OTM.ViewModels.Group;
using Consts = Constants.CoreConfigurationConstants;

namespace OTM.Validators.GroupValidators
{
    public class EditGroupValidator : AbstractValidator<EditGroupViewModel>
    {
        private readonly IGroupsRepository _groupsRepository;
        public EditGroupValidator( IGroupsRepository groupsRepository)

        {
            _groupsRepository = groupsRepository;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(string.Format(Consts.FieldEmptyMessage, "Name"))
                .MaximumLength(Consts.MaxLength)
                .WithMessage(string.Format(Consts.FieldMaximumLengthMessage, "Name", Consts.MaxLength));

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(string.Format(Consts.FieldEmptyMessage, "Description"))
                .MaximumLength(Consts.MaxLength)
                .WithMessage(string.Format(Consts.FieldMaximumLengthMessage, "Description", Consts.MaxLength));
            RuleFor(x => x).Custom((x, context) =>
            {
                var group = _groupsRepository.GetByIdAsync(x.Id).Result;
                if (group == null)
                {
                    context.AddFailure("Id", "Unauthorized");
                }
            });

        }
    }
}   
