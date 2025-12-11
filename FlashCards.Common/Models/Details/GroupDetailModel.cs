using FlashCards.Common.Models.Lists;

namespace FlashCards.Common.Models.Details;

public class GroupDetailModel : GroupListModel
{
    public ICollection<UserListModel> Users { get; set; } = new List<UserListModel>();
}