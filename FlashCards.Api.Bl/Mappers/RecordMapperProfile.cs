using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using System.Linq;

namespace FlashCards.Api.Bl.Mappers;

public class RecordMapperProfile : Profile
{
    public RecordMapperProfile()
    {
        CreateMap<RecordDetailModel, RecordEntity>();
        CreateMap<RecordEntity, RecordDetailModel>()
            .ForMember(dest => dest.NumberOfAnswers,
                opt => opt.MapFrom(src => src.Attempts.Count));

        CreateMap<RecordListModel, RecordEntity>();
        CreateMap<RecordEntity, RecordListModel>()
            .ForMember(dest => dest.NumberOfAnswers,
                opt => opt.MapFrom(src => src.Attempts.Count));
        ;
    }
}