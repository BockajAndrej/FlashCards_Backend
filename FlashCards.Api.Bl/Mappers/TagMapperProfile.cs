using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Mappers;

public class TagMapperProfile : Profile
{
	public TagMapperProfile()
	{
		CreateMap<TagEntity, TagListModel>().ReverseMap();
		CreateMap<TagEntity, TagDetailModel>().ReverseMap();

		CreateMap<TagDetailModel, TagListModel>();
	}
}