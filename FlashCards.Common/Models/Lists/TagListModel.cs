using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class TagListModel : IModel
{
	public Guid Id { get; set; }
	[Required] 
	public string Name { get; set; } = null!;
	
	public Guid UserId { get; set; }
}