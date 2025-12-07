using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Mappers;

public class AttemptMapperProfile : Profile
{
	public AttemptMapperProfile()
	{
		CreateMap<AttemptDetailModel, AttemptEntity>();
		CreateMap<AttemptEntity, AttemptDetailModel>();
		
		CreateMap<AttemptListModel, AttemptEntity>();
		CreateMap<AttemptEntity, AttemptListModel>();
	}
}