using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Create;

namespace FlashCards.Api.Bl.Facades;

public class LessonAttemptFacade : ILessonAttemptFacade
{
	private readonly FlashCardsDbContext _dbContext;
	private readonly IMapper _mapper;

	public LessonAttemptFacade(FlashCardsDbContext dbContext, IMapper mapper)
	{
		_dbContext = dbContext;
		_mapper = mapper;
	}

	public async Task<LessonAttemptCreateModel> LogAttemptAsync(LessonAttemptCreateModel model)
	{
		var entity = _mapper.Map<LessonAttemptEntity>(model);

		entity.AttemptDateTime = DateTime.UtcNow;

		_dbContext.Set<LessonAttemptEntity>().Add(entity);
		await _dbContext.SaveChangesAsync();

		_mapper.Map(entity, model);
		return model;
	}
}