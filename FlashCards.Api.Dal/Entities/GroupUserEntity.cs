using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public record GroupUserEntity : IEntity
{
    public Guid Id { get; set; }
    
    [Required]
    public EnumUserRole Role { get; set; }
    
    [Required]
    public Guid GroupId { get; set; }
    [Required]
    public GroupEntity Group { get; set; } = null!;
    
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public UserEntity User { get; set; } = null!;
}