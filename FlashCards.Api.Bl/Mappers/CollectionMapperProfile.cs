using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Mappers;

public class CollectionMapperProfile : Profile
{
	public CollectionMapperProfile()
	{
		CreateMap<CollectionEntity, CollectionDetailModel>()
			.ForMember(dest => dest.NumberOfCards,
				opt => opt.MapFrom(src => src.Cards.Count))
			.ForMember(dest => dest.Tags,
				opt => opt.MapFrom(src =>
					src.TagBelong.Select(l => l.Tag)));
		CreateMap<CollectionDetailModel, CollectionEntity>();

		CreateMap<CollectionEntity, CollectionListModel>()
			.ForMember(dest => dest.NumberOfCards,
				opt => opt.MapFrom(src => src.Cards.Count))
			.ForMember(dest => dest.Tags,
				opt => opt.MapFrom(src =>
					src.TagBelong.Select(l => l.Tag)));
		CreateMap<CollectionListModel, CollectionEntity>();
	}
}