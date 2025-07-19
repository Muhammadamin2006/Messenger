namespace Messenger.Application.Dtos.UserDtos;

public class RegistrationDto
{

    public string UserName { get; set; } =  null!;
    
    public string Email { get; set; } =  null!;
    
    public string Password { get; set; } =  null!;
    
    public string ConfirmPassword { get; set; } =  null!;
    
}