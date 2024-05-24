using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.RepositoryAbstracts;

public interface IGenericRepository<T> where T : BaseEntity, new()
{
    Task AddEntityAsync(T entity);
    void DeleteEntity(T entity);
    T GetEntity(Func<T,bool>? func=null);
    List<T> GetAllEntities(Func<T, bool>? func = null);
    int Commit();
    Task<int> CommitAsync();

}
