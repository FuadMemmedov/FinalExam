using Core.Models;
using Core.RepositoryAbstracts;
using Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.RepositoryConcretes;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _appDbContext;

    public GenericRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddEntityAsync(T entity)
    {
       await  _appDbContext.Set<T>().AddAsync(entity);
    }

    public int Commit()
    {
        return _appDbContext.SaveChanges();
    }

    public async Task<int> CommitAsync()
    {
       return await _appDbContext.SaveChangesAsync();
    }

    public void DeleteEntity(T entity)
    {
       _appDbContext.Set<T>().Remove(entity);
    }

    public List<T> GetAllEntities(Func<T, bool>? func = null)
    {
       return func == null ? _appDbContext.Set<T>().ToList() : _appDbContext.Set<T>().Where(func).ToList();
    }

    public T GetEntity(Func<T, bool>? func = null)
    {
        return func == null ? _appDbContext.Set<T>().FirstOrDefault() : _appDbContext.Set<T>().FirstOrDefault(func);
    }
}
