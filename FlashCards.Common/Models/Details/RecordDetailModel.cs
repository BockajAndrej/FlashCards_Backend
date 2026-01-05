using FlashCards.Common.Models.Lists;

namespace FlashCards.Common.Models.Details;

public class RecordDetailModel : RecordListModel
{
	public ICollection<AttemptListModel> Attempts { get; set; } = new List<AttemptListModel>();
}