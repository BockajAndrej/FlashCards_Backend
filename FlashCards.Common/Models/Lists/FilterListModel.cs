using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class FilterListModel : IModel
{
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    
    public Guid UserId { get; set; }
    
    public ICollection<TagListModel> Tags { get; set; } = new List<TagListModel>();
}