using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;

namespace FlashCards.Api.Dal.Entities;

public record CollectionTagEntity : IEntity
{
    public Guid Id { get; set; }
    
    [Required] public Guid TagId { get; set; }
    [Required] public TagEntity Tag { get; set; } = null!;
    [Required] public Guid CardCollectionId { get; set; }
    [Required] public CollectionEntity Collection { get; set; } = null!;
}