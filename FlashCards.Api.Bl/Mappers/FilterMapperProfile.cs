using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Mappers;

public class FilterMapperProfile : Profile
{
    public FilterMapperProfile()
    {
        CreateMap<FilterDetailModel, FilterEntity>();
        CreateMap<FilterEntity, FilterDetailModel>()
            .ForMember(dest => dest.Tags,
                opt => opt.MapFrom(src => src.TagsBelong.Select(l => l.Tag)));

        CreateMap<FilterListModel, FilterEntity>();
        CreateMap<FilterEntity, FilterListModel>()
            .ForMember(dest => dest.Tags,
                opt => opt.MapFrom(src => src.TagsBelong.Select(l => l.Tag)));
    }
}