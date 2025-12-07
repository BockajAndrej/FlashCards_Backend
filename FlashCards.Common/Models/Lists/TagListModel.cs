using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class TagListModel : IModel
{
	public Guid Id { get; set; }
	public string Tag { get; set; } 
}