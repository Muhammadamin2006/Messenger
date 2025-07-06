using Messenger.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Messenger.DTOs;

namespace Messenger.Api.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    TEntity GetById(Guid id);
    void Add(TEntity entity, Guid id);
    bool TryDelete(TEntity entity, Guid id);
    bool TryUpdate(TEntity entity,  Guid id);
    
    // void SaveChanges(T entity);


}