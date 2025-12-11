using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class GroupListModel : IModel
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;
    
    [Required]
    public int MaxMembersCount { get; set; }
}