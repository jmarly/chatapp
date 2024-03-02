using System.Collections.Concurrent;

namespace net.applicationperformance.ChatApp.Repositories;
using Models;

public class UserRepository : RepositoryBase<Guid,UserDto>, IUserRepository
{
    public override Guid NewId => Guid.NewGuid();
    public override Guid NullId => Guid.Empty;

    public List<string> GetAllUserNames()
    {
        return GetAll().Select(u => u.UserName).ToList();
    }
}