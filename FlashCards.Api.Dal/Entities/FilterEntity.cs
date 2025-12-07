using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;

namespace FlashCards.Api.Dal.Entities;

public record FilterEntity : IEntity
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public bool IsActive { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public UserEntity User { get; set; } = null!;
    
    public ICollection<FilterTagEntity> TagsBelong { get; set; } = new List<FilterTagEntity>();
}