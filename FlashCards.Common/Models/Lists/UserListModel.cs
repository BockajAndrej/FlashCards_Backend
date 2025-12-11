using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class UserListModel : IModel
{
    public Guid Id { get; set; }

    public string? Name { get; set; }
    public string? UserImageUrl { get; set; }
    public EnumUserRole? Role { get; set; }
    public string? RealUserUrl { get; set; } = null!;
}