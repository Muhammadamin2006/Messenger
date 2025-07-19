using FluentValidation;
using Messenger.Application.Dtos;
using Messenger.Application.Dtos.UserDtos;

namespace Messenger.Presentation.Validations;

public class UserBlockValidation : AbstractValidator<BlockUserDto>
{
    public UserBlockValidation()
    {
        RuleFor(x => x.BlockerId)
            .NotEmpty().WithMessage("Идентификатор блокирующего обязателен");

        RuleFor(x => x.BlockedId)
            .NotEmpty().WithMessage("Идентификатор заблокированного обязателен");

        RuleFor(x => x)
            .Must(x => x.BlockerId != x.BlockedId)
            .WithMessage("Нельзя заблокировать самого себя");
    }
}
