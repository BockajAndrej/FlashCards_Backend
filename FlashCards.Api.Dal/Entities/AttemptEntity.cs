using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public class AttemptEntity : IEntity
{
	public Guid Id { get; set; }
	[Required] public DateTime AttemptDateTime { get; set; }
	[Required] public EnumAnswerType AnswerResultType { get; set; }

	[Required] public Guid RecordId { get; set; }
	public RecordEntity Record { get; set; } = null!;

	[Required] public Guid CardId { get; set; }
	public CardEntity Card { get; set; } = null!;
}