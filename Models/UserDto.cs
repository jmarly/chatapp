namespace net.applicationperformance.ChatApp.Models;

public class UserDto : IDto<Guid>
{
    public UserDto(string userName, string email,string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }

    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
