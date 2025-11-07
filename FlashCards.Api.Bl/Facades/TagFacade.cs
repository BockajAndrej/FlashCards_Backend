using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Facades;

public class TagFacade(FlashCardsDbContext dbContext, IMapper mapper)
	: FacadeBase<TagEntity, TagListModel, TagDetailModel>(dbContext, mapper)
		, ITagFacade
{
}