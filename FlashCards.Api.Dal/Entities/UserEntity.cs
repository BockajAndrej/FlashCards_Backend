using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public record UserEntity : IEntity
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? UserImageUrl { get; set; }
    [Required]
    public EnumUserRole Role { get; set; }
    
    [Required]
    public string RealUserUrl { get; set; } = null!;
    
    public ICollection<CardCollectionEntity> CardCollections { get; set; } = new List<CardCollectionEntity>();
    public ICollection<CompletedLessonEntity> CompletedLessons { get; set; } = new List<CompletedLessonEntity>();
}