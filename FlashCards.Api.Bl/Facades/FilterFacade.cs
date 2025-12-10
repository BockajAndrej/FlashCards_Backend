using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Bl.Facades;

public class FilterFacade(FlashCardsDbContext dbContext, IMapper mapper)
    : FacadeBase<FilterEntity, FilterQueryObject, FilterListModel, FilterDetailModel>(dbContext, mapper),
        IFilterFacade
{
    protected override Task<IQueryable<FilterEntity>> CreateFilterQuery(FilterQueryObject queryObject,
        IQueryable<FilterEntity> query)
    {
        query = query.Where(l => l.UserId == queryObject.CreatedByIdFilter);

        if (queryObject.IsActive.HasValue)
            query = query.Where(l => l.IsActive == queryObject.IsActive.Value);

        return Task.FromResult(query);
    }

    protected override IQueryable<FilterEntity> CreateOrderQuery(FilterQueryObject queryObject,
        IQueryable<FilterEntity> query)
    {
        return query;
    }

    protected override FilterEntity SavaDetail(FilterEntity detail)
    {
        return detail;
    }

    protected override FilterEntity ModifyDetail(FilterEntity detail)
    {
        return detail;
    }

    public override async Task<FilterDetailModel> SaveAsync(FilterDetailModel model)
    {
        var entity = mapper.Map<FilterEntity>(model);

        var currentActiveEntity = await dbContext.Set<FilterEntity>()
            .FirstOrDefaultAsync(e => e.IsActive);

        if (currentActiveEntity != null && currentActiveEntity.Id != entity.Id)
        {
            currentActiveEntity.IsActive = false;
            dbContext.Set<FilterEntity>().Update(currentActiveEntity);
            await dbContext.SaveChangesAsync();
        }

        if (entity.Id == Guid.Empty)
        {
            dbContext.Set<FilterEntity>().Add(entity);
        }
        else
        {
            dbContext.Set<FilterEntity>().Update(entity);
        }

        await dbContext.SaveChangesAsync();

        mapper.Map(entity, model);

        return model;
    }
}