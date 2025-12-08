using FlashCards.Common.Models.Lists;

namespace FlashCards.Common.Models.Details;

public class TagDetailModel : TagListModel
{
    public ICollection<CollectionListModel> Collections { get; set; } = new List<CollectionListModel>();
    public ICollection<FilterListModel> Filters { get; set; } = new List<FilterListModel>();
}