using AutoMapper;
using AutoMapper.QueryableExtensions;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using Microsoft.EntityFrameworkCore;

namespace FlashCards.Api.Bl.Facades;

public class CompletedLessonFacade(FlashCardsDbContext dbContext, IMapper mapper)
	: FacadeBase<CompletedLessonEntity, CompletedLessonListModel, CompletedLessonDetailModel>(dbContext, mapper),
		ICompletedLessonFacade
{
	public async Task<CompletedLessonListModel?> GetLastLessonByCollectionIdAsync(Guid collectionId, string userId)
	{
		var lastLessonEntity = dbContext.Set<CompletedLessonEntity>()
			.Where(lesson => lesson.CardCollectionId == collectionId && lesson.User.RealUserUrl == userId)
			.OrderByDescending(lesson => lesson.CreatedDateTime)
			.ProjectTo<CompletedLessonListModel>(mapper.ConfigurationProvider); 

		if (lastLessonEntity == null)
		{
			return null;
		}

		var result = await lastLessonEntity.FirstOrDefaultAsync();

		return result;
	}
}