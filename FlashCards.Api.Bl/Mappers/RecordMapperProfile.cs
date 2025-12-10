using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using System.Linq;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Bl.Mappers;

public class RecordMapperProfile : Profile
{
    public RecordMapperProfile()
    {
        CreateMap<RecordDetailModel, RecordEntity>();
        CreateMap<RecordEntity, RecordDetailModel>()
            .ForMember(dest => dest.NumberOfAnswers,
                opt => opt.MapFrom(src => src.Attempts.Count))
            .ForMember(dest => dest.NumberOfCorrectAnswers,
                opt => opt.MapFrom(src => src.Attempts.Count(l => l.AnswerResultType == EnumAnswerType.Iknow)))
            .ForMember(dest => dest.NumberOfNotSureAnswers,
                opt => opt.MapFrom(src => src.Attempts.Count(l => l.AnswerResultType == EnumAnswerType.Iamnotsure)))
            .ForMember(dest => dest.NumberOfIncorrectAnswers,
                opt => opt.MapFrom(src => src.Attempts.Count(l => l.AnswerResultType == EnumAnswerType.Idonotknow)));

        CreateMap<RecordListModel, RecordEntity>();
        CreateMap<RecordEntity, RecordListModel>()
            .ForMember(dest => dest.NumberOfAnswers,
                opt => opt.MapFrom(src => src.Attempts.Count))
            .ForMember(dest => dest.NumberOfCorrectAnswers,
                opt => opt.MapFrom(src => src.Attempts.Count(l => l.AnswerResultType == EnumAnswerType.Iknow)))
            .ForMember(dest => dest.NumberOfNotSureAnswers,
                opt => opt.MapFrom(src => src.Attempts.Count(l => l.AnswerResultType == EnumAnswerType.Iamnotsure)))
            .ForMember(dest => dest.NumberOfIncorrectAnswers,
                opt => opt.MapFrom(src => src.Attempts.Count(l => l.AnswerResultType == EnumAnswerType.Idonotknow)));
    }
}