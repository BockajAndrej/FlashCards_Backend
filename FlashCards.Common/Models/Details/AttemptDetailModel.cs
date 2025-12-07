using FlashCards.Common.Models.Lists;

namespace FlashCards.Common.Models.Details;

public class AttemptDetailModel : AttemptListModel
{
    public RecordListModel Record { get; set; } =  null!;
    public CardListModel Card { get; set; } =  null!;
}