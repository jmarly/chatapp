using System.Collections.Concurrent;
namespace net.applicationperformance.ChatApp.Repositories;
using Models;

public abstract class RepositoryBase<TK, T> : IRepositoryService<TK, T> 
    where T : IDto<TK>
    where TK : notnull
     
{

    private readonly ConcurrentDictionary<TK, T> _repo = new ConcurrentDictionary<TK, T>();
    public  abstract TK NewId { get; }
    public abstract TK NullId { get; }

    public TK Add(T? dto)
    {
        if (dto == null)
            return NullId;
        dto.Id = NewId;
        _repo[dto.Id] = dto;
        return dto.Id;
    }

    public T? Get(TK id)
    {
        _repo.TryGetValue(id, out var result);
        return result;
    }

    public IList<T> GetAll()
    {
        return _repo.Values.ToList();
    }

    public T Update(T dto)
    {
        _repo[dto.Id] = dto;
        return dto;
    }


}