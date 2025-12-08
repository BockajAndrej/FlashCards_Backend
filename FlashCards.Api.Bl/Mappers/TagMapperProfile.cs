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
		CreateMap<TagEntity, TagListModel>();
		CreateMap<TagListModel, TagEntity>();

		CreateMap<TagEntity, TagDetailModel>()
			.ForMember(dest => dest.Collections,
				opt => opt.MapFrom(src => src.CollectionBelong.Select(l => l.Collection)))
			.ForMember(dest => dest.Filters,
				opt => opt.MapFrom(src => src.FiltersBelong.Select(l => l.Filter)));
		CreateMap<TagDetailModel, TagEntity>();

		CreateMap<TagDetailModel, TagListModel>();
	}
}