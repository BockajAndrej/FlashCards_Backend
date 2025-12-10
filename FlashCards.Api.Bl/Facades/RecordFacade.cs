using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Bl.Facades;

public class RecordFacade(FlashCardsDbContext dbContext, IMapper mapper)
    : FacadeBase<RecordEntity, RecordQueryObject, RecordListModel, RecordDetailModel>(dbContext, mapper),
        IRecordFacade
{
    public async Task<RecordDetailModel?> GetLastRecordByCollectionIdAsync(Guid collectionId, Guid userId)
    {
        var lastRecord = await dbContext.Record
            .Where(r => r.CardCollectionId == collectionId)
            .OrderByDescending(r => r.CreatedDateTime)
            .Include(l => l.Attempts)
            .FirstOrDefaultAsync();

        if (lastRecord == null)
            return null;

        return mapper.Map<RecordDetailModel>(lastRecord);
    }
    
    public async Task<RecordDetailModel?> GetActiveRecordByCollectionIdAsync(Guid collectionId, Guid userId)
    {
        var lastRecord = await dbContext.Record
            .Where(r => r.CardCollectionId == collectionId && r.IsCompleted == false)
            .OrderByDescending(r => r.CreatedDateTime)
            .FirstOrDefaultAsync();

        if (lastRecord == null)
            return null;

        return mapper.Map<RecordDetailModel>(lastRecord);
    }

    protected override Task<IQueryable<RecordEntity>> CreateFilterQuery(RecordQueryObject queryObject,
        IQueryable<RecordEntity> query)
    {
        if (queryObject.IsCompletedFilter != null)
            query = query.Where(record => record.IsCompleted == queryObject.IsCompletedFilter);

        return Task.FromResult(query);
    }

    protected override IQueryable<RecordEntity> CreateOrderQuery(RecordQueryObject queryObject,
        IQueryable<RecordEntity> query)
    {
        return query;
    }

    protected override RecordEntity SavaDetail(RecordEntity detail)
    {
        return detail;
    }

    protected override RecordEntity ModifyDetail(RecordEntity detail)
    {
        return detail;
    }

    public override async Task<RecordDetailModel> SaveAsync(RecordDetailModel model)
    {
        var entity = mapper.Map<RecordEntity>(model);

        if (entity.Id == Guid.Empty)
        {
            var previousEntity = await dbContext.Set<RecordEntity>()
                .FirstOrDefaultAsync(e => e.IsCompleted == false);

            if (previousEntity != null)
            {
                previousEntity.IsCompleted = true;
                dbContext.Set<RecordEntity>().Update(previousEntity);
            }

            entity.CreatedDateTime = DateTime.Now;
            entity.ModifiedDateTime = DateTime.Now;

            entity.IsCompleted = false;
            
            dbContext.Set<RecordEntity>().Add(entity);
        }
        else
        {
            entity.ModifiedDateTime = DateTime.Now;
            dbContext.Set<RecordEntity>().Update(entity);
        }

        await dbContext.SaveChangesAsync();

        mapper.Map(entity, model);

        return model;
    }
}