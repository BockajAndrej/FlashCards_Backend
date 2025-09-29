using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public class CardEntity : IEntity
{
    public Guid Id { get; set; }
    [Required]
    public EnumCardType QuestionTypeEnum { get; set; }
    [Required]
    public EnumCardType AnswerTypeEnum { get; set; }
    [Required]
    public string Question { get; set; } = null!;
    [Required]
    public string CorrectAnswer { get; set; } = null!;
    public string? Description { get; set; }
    [Required]
    public DateTime LastModifiedDateTime { get; set; }
    
    [Required]
    public Guid CardCollectionId { get; set; }
    [Required]
    public CardCollectionEntity CardCollection { get; set; } = null!;
}