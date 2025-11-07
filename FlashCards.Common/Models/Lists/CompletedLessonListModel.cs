using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class CompletedLessonListModel : IEntityModel
{
    public Guid Id { get; set; }
    [Required]
    public DateTime CreatedDateTime { get; set; }
    [Required]
    public ICollection<int> NumberOfCorrectAnswersByTypes { get; set; } = new List<int>(Enum.GetNames(typeof(EnumCompletedLessonAnswerType)).Length);
    [Required]
    public Guid CardCollectionId { get; set; }
    public CardCollectionListModel? CardCollection { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public UserListModel User { get; set; } = null!;
}