namespace net.applicationperformance.ChatApp.Repositories;
using Models;
public interface IUserRepository : IRepositoryBase<Guid, UserDto>
{

    public List<string> GetAllUserNames();
}