using Messenger.Api.Repositories;
using Messenger.Infrastractures.Database;

namespace Messenger.Repositories;

public class GenericRepository<TEntity>(MessengerContext messengerContext, IGenericRepository<TEntity> iGenericRepository) : IGenericRepository<TEntity> where TEntity : class 
{
    public IQueryable<TEntity> GetAll()
    {
        var getall = messengerContext.Set<TEntity>();
        return getall;
    }

    public TEntity GetById(Guid id)
    {
        var getallbyid = messengerContext.Find<TEntity>(id);
        return iGenericRepository.GetById(id);
    }

    public void Add(TEntity entity,  Guid id)
    {
        var add = messengerContext.Add(entity); 
        messengerContext.SaveChanges();
    }

    public bool TryDelete(TEntity entity, Guid id)
    {
        var delete = messengerContext.Remove(entity);
        messengerContext.SaveChanges();
        return true;
    }

    public bool TryUpdate(TEntity entity, Guid id)
    {
        var update = messengerContext.Update(entity);
        messengerContext.SaveChanges();
        return true;
    }



}