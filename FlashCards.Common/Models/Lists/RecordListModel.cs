using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class RecordListModel : IModel
{
    public Guid Id { get; set; }
    
    public DateTime CreatedDateTime { get; set; }
    public DateTime ModifiedDateTime { get; set; }

    public Guid CardCollectionId { get; set; }
    public CollectionListModel? CardCollection { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public int NumberOfAnswers { get; set; }
    
    public int NumberOfCorrectAnswers { get; set; }
    public int NumberOfIncorrectAnswers { get; set; }
    public int NumberOfNotSureAnswers { get; set; }
    
    public Guid? UserId { get; set; }
}