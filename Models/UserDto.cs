namespace net.applicationperformance.ChatApp.Models;

public class UserDto(Guid id, string userName, string email, string password)
    : IDto<Guid>
{
    public Guid Id { get; set; } = id;
    public string UserName { get; set; } = userName;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
}
