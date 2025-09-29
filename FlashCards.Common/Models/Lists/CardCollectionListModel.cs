using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class CardCollectionListModel : IEntityModel
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    public DateTime? StartTimeForAcceptedAnswers { get; set; }
    public DateTime? EndTimeForAcceptedAnswers { get; set; }
    public DateTime? LastModifiedDateTime { get; set; }
    public DateTime? LastPlayedDateTime { get; set; }
    public string? UserId { get; set; }
}