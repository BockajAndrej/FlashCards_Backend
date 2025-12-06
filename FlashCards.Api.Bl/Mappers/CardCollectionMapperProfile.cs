using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Mappers;

public class CardCollectionMapperProfile : Profile
{
	public CardCollectionMapperProfile()
	{
		CreateMap<CardCollectionEntity, CardCollectionDetailModel>()
			.ForMember(dest => dest.NumberOfCards,
				opt => opt.MapFrom(src => src.Cards.Count));
		CreateMap<CardCollectionDetailModel, CardCollectionEntity>();

		CreateMap<CardCollectionEntity, CardCollectionListModel>()
			.ForMember(dest => dest.NumberOfCards,
				opt => opt.MapFrom(src => src.Cards.Count));
		CreateMap<CardCollectionListModel, CardCollectionEntity>();
	}
}