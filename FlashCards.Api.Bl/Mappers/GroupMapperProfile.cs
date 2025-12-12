using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Mappers;

public class GroupMapperProfile :  Profile
{
    public GroupMapperProfile()
    {
        CreateMap<GroupDetailModel, GroupEntity>();
        CreateMap<GroupEntity, GroupDetailModel>()
            .ForMember(dest => dest.Users,
                opt => opt.MapFrom(src => src.UsersBelong.Select(l => l.User)))
            .ForMember(dest => dest.NumberOfMembers,
                opt => opt.MapFrom(src => src.UsersBelong.Count));

        CreateMap<GroupListModel, GroupEntity>();
        CreateMap<GroupEntity, GroupListModel>()
            .ForMember(dest => dest.NumberOfMembers,
                opt => opt.MapFrom(src => src.UsersBelong.Count));
    }
}