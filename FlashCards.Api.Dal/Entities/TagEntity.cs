using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;

namespace FlashCards.Api.Dal.Entities;

public class TagEntity : IEntity
{
	public Guid Id { get; set; }
	[Required] public string Tag { get; set; } = null!;
	public ICollection<CardCollectionEntity> CardCollections { get; set; } = new List<CardCollectionEntity>();
}