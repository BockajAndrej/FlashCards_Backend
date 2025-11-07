using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public record CompletedLessonEntity : IEntity
{
	public Guid Id { get; set; }
	[Required] public DateTime CreatedDateTime { get; set; }

	// TODO delete this atribute, but its being used in tests
	[Required]
	public ICollection<int> NumberOfCorrectAnswersByTypes { get; set; } =
		new List<int>(Enum.GetNames(typeof(EnumCompletedLessonAnswerType)).Length);

	// ! new atribute instead of the upper one
	public ICollection<LessonAttemptEntity> LessonAttempts { get; set; } = new List<LessonAttemptEntity>();

	[Required] public Guid CardCollectionId { get; set; }
	public CardCollectionEntity? CardCollection { get; set; }
	[Required] public Guid UserId { get; set; }
	[Required] public UserEntity User { get; set; } = null!;
}