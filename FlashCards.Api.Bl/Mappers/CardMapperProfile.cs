using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Mappers;

public class CardMapperProfile : Profile
{
    public CardMapperProfile()
    {
        CreateMap<CardEntity, CardDetailModel>();
        CreateMap<CardDetailModel, CardEntity>();
        
        CreateMap<CardEntity, CardListModel>();
        CreateMap<CardListModel, CardEntity>();
    }
}