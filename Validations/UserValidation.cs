using Messenger.DTOs;
using FluentValidation;
namespace Messenger.Validations;

public class UserValidation : AbstractValidator<UserDto>
{
    public UserValidation()
    {
        RuleFor(userDto => userDto.UserName)
            .NotEmpty()
            .MaximumLength(30);
        RuleFor(userDto => userDto.Password)
            .NotEmpty()
            .MinimumLength(6);
        RuleFor(userDto => userDto.Email)
            .NotEmpty()
            .EmailAddress();
        

    }
}