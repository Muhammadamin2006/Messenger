using FluentValidation;
using Messenger.Application.Dtos;
using Messenger.Application.Dtos.MessageDtos;

namespace Messenger.Presentation.Validations;

public class CreateOutgoingMessageValidation : AbstractValidator<CreateOutgoingMessageDto>
{
    public CreateOutgoingMessageValidation()
    {
        RuleFor(x => x.SenderId)
            .NotEmpty().WithMessage("SenderId обязателен.");

        RuleFor(x => x.Text)
            .NotEmpty().WithMessage("Текст сообщения не может быть пустым.")
            .MaximumLength(1000).WithMessage("Текст сообщения слишком длинный.");

        RuleFor(x => x)
            .Must(dto =>
                dto.ReceiverUserId != null ^ dto.ReceiverGroupId != null) 
            .WithMessage("Должен быть указан либо ReceiverUserId, либо ReceiverGroupId, но не оба.");

    }
}