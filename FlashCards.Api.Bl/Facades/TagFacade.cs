using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Bl.Facades;

public class TagFacade(FlashCardsDbContext dbContext, IMapper mapper)
    : FacadeBase<TagEntity, TagQueryObject, TagListModel, TagDetailModel>(dbContext, mapper)
        , ITagFacade
{
    protected override Task<IQueryable<TagEntity>> CreateFilterQuery(TagQueryObject queryObject,
        IQueryable<TagEntity> query)
    {
        query = query.Where(l => l.UserId == queryObject.CreatedByIdFilter);
        return Task.FromResult(query);
    }

    protected override IQueryable<TagEntity> CreateOrderQuery(TagQueryObject queryObject, IQueryable<TagEntity> query)
    {
        query = query.OrderBy(tag => tag.Name);
        return query;
    }

    protected override TagEntity SavaDetail(TagEntity detail)
    {
        return detail;
    }

    protected override TagEntity ModifyDetail(TagEntity detail)
    {
        return detail;
    }

    public override async Task<TagDetailModel> SaveAsync(TagDetailModel model)
    {
        var entity = mapper.Map<TagEntity>(model);

        if (entity.Id == Guid.Empty)
        {
            dbContext.Set<TagEntity>().Add(entity);
            
            var newFilterIds = model.Filters.Select(e => e.Id).ToList();
            var newCollectionIds = model.Collections.Select(e => e.Id).ToList();

            foreach (var filterId in newFilterIds)
            {
                var newJoinEntity = new FilterTagEntity()
                {
                    TagId = entity.Id,
                    FilterId = filterId
                };
                dbContext.Set<FilterTagEntity>().Add(newJoinEntity);
            }
            
            foreach (var collectionId in newCollectionIds)
            {
                var newJoinEntity = new CollectionTagEntity()
                {
                    TagId = entity.Id,
                    CollectionId = collectionId
                };
                dbContext.Set<CollectionTagEntity>().Add(newJoinEntity);
            }
            
        }
        else
        {
            var existingTag = await dbContext.Set<TagEntity>()
                .Include(t => t.CollectionBelong)
                .Include(t => t.FiltersBelong)
                .FirstOrDefaultAsync(t => t.Id == entity.Id);

            if (existingTag == null)
            {
                throw new InvalidOperationException($"Tag s ID {entity.Id} was not found.");
            }

            dbContext.Entry(existingTag).CurrentValues.SetValues(entity);

            var incomingCollectionIds = model.Collections.Select(c => c.Id).ToHashSet();

            var collectionsToRemove = existingTag.CollectionBelong
                .Where(ct => !incomingCollectionIds.Contains(ct.CollectionId))
                .ToList();

            dbContext.Set<CollectionTagEntity>().RemoveRange(collectionsToRemove);

            var existingCollectionIds = existingTag.CollectionBelong.Select(ct => ct.CollectionId).ToHashSet();

            var collectionsToAddIds = incomingCollectionIds
                .Where(id => !existingCollectionIds.Contains(id))
                .ToList();

            foreach (var id in collectionsToAddIds)
            {
                existingTag.CollectionBelong.Add(new CollectionTagEntity
                {
                    TagId = existingTag.Id,
                    CollectionId = id
                });
            }

            var incomingFilterIds = model.Filters.Select(f => f.Id).ToHashSet();

            var filtersToRemove = existingTag.FiltersBelong
                .Where(ft => !incomingFilterIds.Contains(ft.FilterId))
                .ToList();

            dbContext.Set<FilterTagEntity>().RemoveRange(filtersToRemove);

            var existingFilterIds = existingTag.FiltersBelong.Select(ft => ft.FilterId).ToHashSet();

            var filtersToAddIds = incomingFilterIds
                .Where(id => !existingFilterIds.Contains(id))
                .ToList();

            foreach (var id in filtersToAddIds)
            {
                existingTag.FiltersBelong.Add(new FilterTagEntity
                {
                    TagId = existingTag.Id,
                    FilterId = id
                });
            }
        }

        await dbContext.SaveChangesAsync();

        mapper.Map(entity, model);

        return model;
    }
}