using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;

namespace FlashCards.Api.Dal.Entities;

public record GroupEntity : IEntity
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int MaxMembersCount { get; set; }
    
    public ICollection<GroupUserEntity> UsersBelong { get; set; } = new List<GroupUserEntity>();
    public ICollection<CollectionEntity> Collections { get; set; } = new List<CollectionEntity>();
}