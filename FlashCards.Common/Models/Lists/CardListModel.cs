using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class CardListModel : IEntityModel
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
    public DateTime? LastModifiedDateTime { get; set; }
}