using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class AttemptListModel : IModel
{
    public Guid Id { get; set; }
    
    public DateTime AttemptDateTime { get; set; }
    public EnumAnswerType AnswerResultType { get; set; }

    public Guid? CompletedLessonId { get; set; }
    public Guid? CardId { get; set; }
}