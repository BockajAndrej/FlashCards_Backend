using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class GroupListModel : IModel
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    public int MaxMembersCount { get; set; }
    
    public EnumUserRole? OwnRole { get; set; }
    public int NumberOfMembers { get; set; }
}