using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;

namespace FlashCards.Api.Dal.Entities;

public class FilterTagEntity : IEntity
{
    public Guid Id { get; set; }
    
    [Required]
    public Guid FilterId { get; set; }
    [Required]
    public FilterEntity Filter { get; set; } = null!;
    
    [Required]
    public Guid TagId { get; set; }
    [Required]
    public TagEntity Tag { get; set; } = null!;
}