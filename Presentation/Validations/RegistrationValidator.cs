using System.Text.RegularExpressions;
using FluentValidation;
using Messenger.Application.Dtos;

namespace Messenger.Presentation.Validations;

public class RegistrationValidator : AbstractValidator<RegisterUserDto>
{
    public RegistrationValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Имя обязательно")
            .MaximumLength(50).WithMessage("Имя слишком длинное");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email обязателен")
            .EmailAddress().WithMessage("Неверный формат Email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен")
            .MinimumLength(6).WithMessage("Пароль слишком короткий");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Пароли не совпадают");
        
    }
    
    private bool IsValidPhoneNumber(string phoneNumber)
    {
        return Regex.IsMatch(phoneNumber, @"^\+\d{10,15}$");
    }
}