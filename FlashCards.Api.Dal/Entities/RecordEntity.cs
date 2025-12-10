using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public record RecordEntity : IEntity
{
	public Guid Id { get; set; }
	[Required] public DateTime CreatedDateTime { get; set; }
	[Required] public DateTime ModifiedDateTime { get; set; }
	
	[Required] public Guid CardCollectionId { get; set; }
	public CollectionEntity? CardCollection { get; set; }
	[Required] public Guid UserId { get; set; }
	[Required] public UserEntity User { get; set; } = null!;
	
	[Required] public bool IsCompleted { get; set; }
	
	public ICollection<AttemptEntity> Attempts { get; set; } = new List<AttemptEntity>();
}