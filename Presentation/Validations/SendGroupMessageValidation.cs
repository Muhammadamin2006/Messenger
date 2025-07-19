using FluentValidation;
using Messenger.Application.Dtos.GroupDto;

namespace Messenger.Presentation.Validations;

public class SendGroupMessageValidation : AbstractValidator<CreateGroupMessageDto>
{
    public SendGroupMessageValidation()
    {
        RuleFor(x => x.GroupId).NotEmpty();
        RuleFor(x => x.SenderId).NotEmpty();
        RuleFor(x => x.Text).NotEmpty().MaximumLength(1000);
    }
}