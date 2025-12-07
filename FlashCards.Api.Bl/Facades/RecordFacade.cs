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
		ICompletedLessonFacade
{
	public async Task<RecordListModel?> GetLastRecordByCollectionIdAsync(Guid collectionId, string userId)
	{
		var lastLessonEntity = dbContext.Set<RecordEntity>()
			.Where(lesson => lesson.CardCollectionId == collectionId && lesson.User.RealUserUrl == userId)
			.OrderByDescending(lesson => lesson.CreatedDateTime)
			.ProjectTo<RecordListModel>(mapper.ConfigurationProvider); 

		if (lastLessonEntity == null)
		{
			return null;
		}

		var result = await lastLessonEntity.FirstOrDefaultAsync();

		return result;
	}

	protected override Task<IQueryable<RecordEntity>> CreateFilterQuery(RecordQueryObject queryObject, IQueryable<RecordEntity> query)
	{
		return Task.FromResult(query);
	}

	protected override IQueryable<RecordEntity> CreateOrderQuery(RecordQueryObject queryObject, IQueryable<RecordEntity> query)
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
}