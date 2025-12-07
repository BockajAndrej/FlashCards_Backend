using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;

namespace FlashCards.Api.Dal.Entities;

public record TagEntity : IEntity
{
	public Guid Id { get; set; }
	[Required] public string Name { get; set; } = null!;
	
	public ICollection<CollectionTagEntity> CollectionBelong { get; set; } = new List<CollectionTagEntity>();
	public ICollection<FilterEntity> Filters { get; set; } = new List<FilterEntity>();
}