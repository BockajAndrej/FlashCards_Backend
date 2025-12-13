using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public record UserEntity : IEntity
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? UserImageUrl { get; set; }
    public string? RealUserUrl { get; set; }
    
    [Required] public EnumUserRole Role { get; set; }
    
    public ICollection<CollectionEntity> Collections { get; set; } = new List<CollectionEntity>();
    public ICollection<RecordEntity> Records { get; set; } = new List<RecordEntity>();
    public ICollection<FilterEntity> Filters { get; set; } = new List<FilterEntity>();
    public ICollection<GroupUserEntity> UsersBelong { get; set; } = new List<GroupUserEntity>();
    
    public ICollection<TagEntity> Tags { get; set; } = new List<TagEntity>();
}