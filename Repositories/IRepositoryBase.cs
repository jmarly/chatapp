namespace net.applicationperformance.ChatApp.Repositories;
using Models;
public interface IRepositoryBase<TK, T> : IRepositoryService<TK, T> 
    where T : IDto<TK>
    where TK : notnull
{
}