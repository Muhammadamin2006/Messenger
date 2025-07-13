using FluentValidation;
using Messenger.Application.Dtos;

namespace Messenger.Presentation.Validations;

public class SearchContactValidation : AbstractValidator<SearchContactDto>
{
    public SearchContactValidation()
    {
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Номер телефона обязателен")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Номер телефона должен быть в формате E.164");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Имя обязательно")
            .MaximumLength(100).WithMessage("Имя не должно превышать 100 символов");
    }
}