using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;

namespace FlashCards.Api.Bl.Facades;

public class TagFacade(FlashCardsDbContext dbContext, IMapper mapper)
	: FacadeBase<TagEntity, TagQueryObject, TagListModel, TagDetailModel>(dbContext, mapper)
		, ITagFacade
{
	protected override IQueryable<TagEntity> CreateFilterQuery(TagQueryObject queryObject, IQueryable<TagEntity> query)
	{
		return query;
	}

	protected override IQueryable<TagEntity> CreateOrderQuery(TagQueryObject queryObject, IQueryable<TagEntity> query)
	{
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
}