using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;

namespace FlashCards.Api.Dal.Entities;

public class CardCollectionEntity : IEntity
{
    public Guid Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    public DateTime? StartTimeForAcceptedAnswers { get; set; }
    public DateTime? EndTimeForAcceptedAnswers { get; set; }
    [Required]
    public DateTime LastModifiedDateTime { get; set; }
    public DateTime? LastPlayedDateTime { get; set; }
    
    [Required]
    public string UserId { get; set; } = null!;
    
    public ICollection<CardEntity> Cards { get; set; } = new List<CardEntity>();
    [Required]
    public ICollection<CompletedLessonEntity> CompletedLessons { get; set; } = new List<CompletedLessonEntity>();
}