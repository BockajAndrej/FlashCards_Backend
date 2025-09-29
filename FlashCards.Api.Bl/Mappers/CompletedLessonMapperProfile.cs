using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Mappers;

public class CompletedLessonMapperProfile : Profile
{
    public CompletedLessonMapperProfile()
    {
        CreateMap<CardCollectionEntity, CardCollectionDetailModel>();
        CreateMap<CardCollectionDetailModel, CardCollectionEntity>();
        
        CreateMap<CardCollectionEntity, CardCollectionListModel>();
        CreateMap<CardCollectionListModel, CardCollectionEntity>();
    }
}