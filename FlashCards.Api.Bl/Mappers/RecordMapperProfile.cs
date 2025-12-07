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
		CreateMap<RecordEntity, RecordListModel>()
			.ForMember(
				destination => destination.User,
				options => options.MapFrom(source => source.User) // Mapujeme priamo objekt
			);

		CreateMap<RecordEntity, RecordDetailModel>();

		CreateMap<RecordListModel, RecordEntity>()
			.ForMember(
				dest => dest.UserId,
				opt => opt.MapFrom(src =>
					src.User != null
						? src.User.Id
						: Guid.Empty)
			)
			.ForMember(
				dest => dest.User,
				opt => opt.Ignore()
			);

		CreateMap<RecordDetailModel, RecordEntity>();
	}
}