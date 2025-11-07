using FlashCards.Common.Models.Lists;

namespace FlashCards.Common.Models.Details;

public class CardCollectionDetailModel : CardCollectionListModel
{
	public ICollection<CardListModel> Cards { get; set; } = new List<CardListModel>();
	public ICollection<CompletedLessonListModel> CompletedLessons { get; set; } = new List<CompletedLessonListModel>();
}