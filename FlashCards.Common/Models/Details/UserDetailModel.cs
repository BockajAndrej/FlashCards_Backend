using FlashCards.Common.Models.Lists;

namespace FlashCards.Common.Models.Details;

public class UserDetailModel : UserListModel
{
    public ICollection<GroupListModel> Groups { get; set; } = new List<GroupListModel>();
}