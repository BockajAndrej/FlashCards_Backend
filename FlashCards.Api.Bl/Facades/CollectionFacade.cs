using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;

namespace FlashCards.Api.Bl.Facades;

public class CollectionFacade(FlashCardsDbContext dbContext, IMapper mapper) 
    : FacadeBase<CollectionEntity, CollectionQueryObject, CollectionListModel, CollectionDetailModel>(dbContext, mapper), 
        ICardCollectionFacade
{
    protected override IQueryable<CollectionEntity> CreateFilterQuery(CollectionQueryObject queryObject, IQueryable<CollectionEntity> query)
    {
        if (queryObject.Name != null)
        {
            query = query.Where(l => l.Title.ToLower().Contains(queryObject.Name.ToLower()));
        }
        if (queryObject.TagIds != null && queryObject.TagIds.Any())
        {
            foreach (var queryObjectTagId in queryObject.TagIds)
            {
                query = query.Where(l => l.TagBelong.Any(belong => belong.TagId == queryObjectTagId));
            }
        }
        return query;
    }

    protected override IQueryable<CollectionEntity> CreateOrderQuery(CollectionQueryObject queryObject, IQueryable<CollectionEntity> query)
    {
        
        return query;
    }

    protected override CollectionEntity SavaDetail(CollectionEntity detail)
    {
        return detail;
    }

    protected override CollectionEntity ModifyDetail(CollectionEntity detail)
    {
        return detail;
    }
}