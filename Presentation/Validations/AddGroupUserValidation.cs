using FluentValidation;
using Messenger.Application.Dtos.GroupDto;

namespace Messenger.Presentation.Validations;

public class AddGroupUserValidation : AbstractValidator<AddGroupUserDto>
{
    public AddGroupUserValidation()
    {
        RuleFor(x => x.GroupId).NotEmpty().WithMessage("GroupId обязателен.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId обязателен.");
    }
}