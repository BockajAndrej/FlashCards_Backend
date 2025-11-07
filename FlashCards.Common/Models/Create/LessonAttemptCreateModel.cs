using FlashCards.Common.Enums;

namespace FlashCards.Common.Models.Create;

public class LessonAttemptCreateModel
{
	public Guid Id { get; set; }
	public Guid CompletedLessonId { get; set; }
	public Guid CardId { get; set; }

	public EnumCompletedLessonAnswerType AnswerResultType { get; set; }
}