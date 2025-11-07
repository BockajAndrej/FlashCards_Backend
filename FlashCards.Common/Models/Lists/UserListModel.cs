using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class UserListModel : IEntityModel
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? UserImageUrl { get; set; }
    [Required]
    public EnumUserRole Role { get; set; }
    
    [Required]
    public string RealUserUrl { get; set; } = null!;
}