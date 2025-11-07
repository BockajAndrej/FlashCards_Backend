using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Mappers;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserEntity, UserDetailModel>();
        CreateMap<UserDetailModel, UserEntity>();
        
        CreateMap<UserEntity, UserListModel>();
        CreateMap<UserListModel, UserDetailModel>();
    }
}