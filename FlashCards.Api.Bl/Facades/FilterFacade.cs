using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;

namespace FlashCards.Api.Bl.Facades;

public class FilterFacade(FlashCardsDbContext dbContext, IMapper mapper) 
    : FacadeBase<FilterEntity, FilterQueryObject, FilterListModel, FilterDetailModel>(dbContext, mapper), 
        IFilterFacade
{
    protected override Task<IQueryable<FilterEntity>> CreateFilterQuery(FilterQueryObject queryObject, IQueryable<FilterEntity> query)
    {
        query = query.Where(l => l.UserId == queryObject.CreatedById);
        
        if(queryObject.IsActive.HasValue)
            query = query.Where(l => l.IsActive == queryObject.IsActive.Value);
            
        return Task.FromResult(query);
    }

    protected override IQueryable<FilterEntity> CreateOrderQuery(FilterQueryObject queryObject, IQueryable<FilterEntity> query)
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
}