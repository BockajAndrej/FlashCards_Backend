using FlashCards.Common.Models.Lists;

namespace FlashCards.Common.Models.Details;

public class CollectionDetailModel : CollectionListModel
{
	public ICollection<CardListModel> Cards { get; set; } = new List<CardListModel>();
	public ICollection<RecordListModel> CompletedLessons { get; set; } = new List<RecordListModel>();
}