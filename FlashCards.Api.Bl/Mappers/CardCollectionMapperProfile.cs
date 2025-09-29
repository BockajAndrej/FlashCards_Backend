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
        CreateMap<CompletedLessonEntity, CompletedLessonDetailModel>();
        CreateMap<CompletedLessonDetailModel, CompletedLessonEntity>();
        
        CreateMap<CompletedLessonEntity, CompletedLessonListModel>();
        CreateMap<CompletedLessonListModel, CompletedLessonEntity>();
    }
}