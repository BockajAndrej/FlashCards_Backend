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
			.ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedById));
		CreateMap<CardCollectionDetailModel, CardCollectionEntity>()
			.ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedById));

		CreateMap<CardCollectionEntity, CardCollectionListModel>()
			.ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedById));
		CreateMap<CardCollectionListModel, CardCollectionEntity>()
			.ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedById));
	}
}