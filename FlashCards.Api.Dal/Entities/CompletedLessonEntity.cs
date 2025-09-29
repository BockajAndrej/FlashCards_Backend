using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public class CompletedLessonEntity : IEntity
{
    public Guid Id { get; set; }
    [Required]
    public DateTime CreatedDateTime { get; set; }
    [Required]
    //It is designed to be used with EnumAbsolvovaneLekceTypOdpovedi
    public ICollection<int> NumberOfCorrectAnswersByTypes { get; set; } = new List<int>(Enum.GetNames(typeof(EnumCompletedLessonAnswerType)).Length);

    [Required]
    public Guid CardCollectionId { get; set; }
    [Required]
    public CardCollectionEntity CardCollection { get; set; } = null!;
    [Required]
    public string UserId { get; set; } = null!;
}