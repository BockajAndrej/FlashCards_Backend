using AutoMapper;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Create;

namespace FlashCards.Api.Bl.Mappers;

public class LessonAttemptMapperProfile : Profile
{
	public LessonAttemptMapperProfile()
	{
		CreateMap<LessonAttemptCreateModel, LessonAttemptEntity>()
			.ForMember(dest => dest.AttemptDateTime, opt => opt.Ignore());

		CreateMap<LessonAttemptEntity, LessonAttemptCreateModel>();
	}
}