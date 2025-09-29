using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Common.Models.Details;

public class CardDetailModel : CardListModel
{
    [Required]
    public Guid CardCollectionId { get; set; }
    public CardCollectionListModel? CardCollection { get; set; }
}