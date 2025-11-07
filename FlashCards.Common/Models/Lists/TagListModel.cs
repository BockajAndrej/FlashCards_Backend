using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class TagListModel : IEntityModel
{
	public Guid Id { get; set; }
	public string Tag { get; set; } 
}