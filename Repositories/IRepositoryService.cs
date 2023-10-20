namespace net.applicationperformance.ChatApp.Repositories;
using Models;

public interface IRepositoryService<TK,T> where T : IDto<TK>
{
    public abstract TK NewId { get; }
    public abstract TK NullId { get; }
    public TK Add(T? dto);
    public T? Get(TK id);
    public IList<T> GetAll();
    public T Update(T dto);
}