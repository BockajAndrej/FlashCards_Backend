using FlashCards.Common.Models.Create;

namespace FlashCards.Api.Bl.Facades.Interfaces;

public interface ILessonAttemptFacade
{
	Task<LessonAttemptCreateModel> LogAttemptAsync(LessonAttemptCreateModel model);
}