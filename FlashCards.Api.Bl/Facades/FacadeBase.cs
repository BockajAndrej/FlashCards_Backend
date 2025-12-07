using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Models.Interfaces;
using FlashCards.Common.QueryObjects.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Bl.Facades;

public abstract class FacadeBase
    <TEntity, TQueryObject, TListModel, TDetailModel>(FlashCardsDbContext dbContext, IMapper mapper)
    : IFacade<TEntity, TQueryObject, TListModel, TDetailModel>
    where TEntity : class, IEntity
    where TDetailModel : class, IModel
    where TQueryObject : class, IQueryObject
{
    protected abstract IQueryable<TEntity> CreateFilterQuery(TQueryObject queryObject, IQueryable<TEntity> query);
    protected abstract IQueryable<TEntity> CreateOrderQuery(TQueryObject queryObject, IQueryable<TEntity> query);
    protected abstract TEntity SavaDetail(TEntity detail);
    protected abstract TEntity ModifyDetail(TEntity detail);
    
    public async Task<IQueryable<TListModel>> GetAsync(TQueryObject queryObject)
    {
        IQueryable<TEntity> query = dbContext.Set<TEntity>();
        
        query = CreateFilterQuery(queryObject, query);
        query = CreateOrderQuery(queryObject, query);
        
        if(queryObject is { PageNumber: not null, PageSize: not null })
            query = query.Skip((queryObject.PageNumber.Value - 1) * queryObject.PageSize.Value).Take(queryObject.PageSize.Value);
        
        return mapper.ProjectTo<TListModel>(query);
    }

    public async Task<TDetailModel?> GetByIdAsync(Guid id)
    {
        IQueryable<TEntity> query = dbContext.Set<TEntity>();
        var projectedQuery = mapper.ProjectTo<TDetailModel>(query);
        return await projectedQuery.FirstOrDefaultAsync(e => e.Id == id);
    }
    
    public async Task<TDetailModel> SaveAsync(TDetailModel model)
    {
        var entity = mapper.Map<TEntity>(model);
        
        var idProperty = entity.GetType().GetProperty("Id");

        var idValue = (Guid)(idProperty?.GetValue(entity) ?? throw new InvalidOperationException());

        if (idValue == Guid.Empty)
        {
            dbContext.Set<TEntity>().Add(entity);
        }
        else
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        await dbContext.SaveChangesAsync();
        
        mapper.Map(entity, model);

        return model;
    }

    public async Task<TDetailModel?> DeleteAsync(Guid entityId)
    {
        TEntity? entity = await dbContext.Set<TEntity>().FindAsync(entityId);
        if (entity != null)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
            return mapper.Map<TEntity, TDetailModel>(entity);
        }
        return null;
    }

    public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = dbContext.Set<TEntity>();
        if (filter != null)
            query = query.Where(filter);
        return await query.CountAsync();
    }
}