using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Enums;
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
        filterQuery.CreatedByIdFilter = queryObject.CreatedByIdFilter;
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

        if (queryObject.OwnCollectionFilter)
        {
            query = query.Where(l => l.CreatedById == queryObject.CreatedByIdFilter);
        }

        if (queryObject.VisibilityFilter.HasValue)
        {
            if(queryObject.VisibilityFilter == EnumCardVisibilityType.Private)
                query = query.Where(l => l.Visibility == EnumCardVisibilityType.Private);
            else if (queryObject.VisibilityFilter == EnumCardVisibilityType.Public)
                query = query.Where(l => l.Visibility == EnumCardVisibilityType.Public);
        }
        else
            query = query.Where(l => l.CreatedById == queryObject.CreatedByIdFilter);

        if (queryObject.NameFilter != null)
        {
            query = query.Where(l => l.Title.ToLower().Contains(queryObject.NameFilter.ToLower()));
        }

        if (queryObject.TagIdsFilter != null && queryObject.TagIdsFilter.Any())
        {
            foreach (var queryObjectTagId in queryObject.TagIdsFilter)
            {
                query = query.Where(l => l.TagBelong.Any(belong => belong.TagId == queryObjectTagId));
            }
        }

        return query;
    }

    protected override IQueryable<CollectionEntity> CreateOrderQuery(CollectionQueryObject queryObject,
        IQueryable<CollectionEntity> query)
    {
        if (queryObject.NameOrder != null)
            query = queryObject.IsDescending ? query.OrderByDescending(l => l.Title) : query.OrderBy(l => l.Title);
        else if (queryObject.RecentOrder != null)
            query = queryObject.IsDescending
                ? query.OrderByDescending(l =>
                    l.Records.Any() ? l.Records.Max(r => r.CreatedDateTime) : (DateTime?)null)
                : query.OrderBy(l => l.Records.Any() ? l.Records.Max(r => r.CreatedDateTime) : (DateTime?)null);

        return query;
    }

    protected override CollectionEntity SavaDetail(CollectionEntity detail)
    {
        detail.LastModifiedDateTime = DateTime.Now;
        return detail;
    }

    protected override CollectionEntity ModifyDetail(CollectionEntity detail)
    {
        detail.LastModifiedDateTime = DateTime.Now;
        return detail;
    }
}