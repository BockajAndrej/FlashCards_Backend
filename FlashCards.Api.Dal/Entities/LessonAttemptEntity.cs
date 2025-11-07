using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public class LessonAttemptEntity : IEntity
{
	public Guid Id { get; set; }
	[Required] public DateTime AttemptDateTime { get; set; }
	[Required] public EnumCompletedLessonAnswerType AnswerResultType { get; set; }

	[Required] public Guid CompletedLessonId { get; set; }
	public CompletedLessonEntity CompletedLesson { get; set; } = null!;

	[Required] public Guid CardId { get; set; }
	public CardEntity Card { get; set; } = null!;
}