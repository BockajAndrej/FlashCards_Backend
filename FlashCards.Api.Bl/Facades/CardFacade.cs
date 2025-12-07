using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;

namespace FlashCards.Api.Bl.Facades;

public class CardFacade(FlashCardsDbContext dbContext, IMapper mapper)
	: FacadeBase<CardEntity, CardQueryObject, CardListModel, CardDetailModel>(dbContext, mapper)
		, ICardFacade
{
	protected override Task<IQueryable<CardEntity>> CreateFilterQuery(CardQueryObject queryObject, IQueryable<CardEntity> query)
	{
		return Task.FromResult(query);
	}

	protected override IQueryable<CardEntity> CreateOrderQuery(CardQueryObject queryObject, IQueryable<CardEntity> query)
	{
		return query;
	}

	protected override CardEntity SavaDetail(CardEntity detail)
	{
		return detail;
	}

	protected override CardEntity ModifyDetail(CardEntity detail)
	{
		return detail;
	}
}