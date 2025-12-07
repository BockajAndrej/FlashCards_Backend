using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;

namespace FlashCards.Api.Bl.Facades;

public class CollectionFacade(FlashCardsDbContext dbContext, IMapper mapper, IFilterFacade filterFacade)
    : FacadeBase<CollectionEntity, CollectionQueryObject, CollectionListModel, CollectionDetailModel>(dbContext,
            mapper),
        ICollectionFacade
{
    protected override async Task<IQueryable<CollectionEntity>> CreateFilterQuery(CollectionQueryObject queryObject,
        IQueryable<CollectionEntity> query)
    {
        FilterQueryObject filterQuery = new FilterQueryObject();
        filterQuery.CreatedById = queryObject.CreatedById;
        filterQuery.IsActive = true;
        var filterListModelsQueryable = await filterFacade.GetAsync(filterQuery);
        var filterListModels = filterListModelsQueryable.ToList();
        
        if (filterListModels.Any())
        {
            var tagListModels = filterListModels.FirstOrDefault()?.Tags;
            if (tagListModels != null)
            {
                foreach (var tagListModel in tagListModels)
                {
                    query = query.Where(l => l.TagBelong.Any(belong => belong.TagId == tagListModel.Id));
                }
            }
        }

        query = query.Where(l => l.CreatedById == queryObject.CreatedById);
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

    protected override IQueryable<CollectionEntity> CreateOrderQuery(CollectionQueryObject queryObject,
        IQueryable<CollectionEntity> query)
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