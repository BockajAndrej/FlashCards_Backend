using System.ComponentModel.DataAnnotations;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Interfaces;

namespace FlashCards.Common.Models.Lists;

public class CollectionListModel : IModel
{
	public Guid Id { get; set; }
	public string Title { get; set; }
	public string? Description { get; set; }

	public DateTime? StartTimeForAcceptedAnswers { get; set; }
	public DateTime? EndTimeForAcceptedAnswers { get; set; }
	public DateTime LastModifiedDateTime { get; set; }
	public DateTime? LastPlayedDateTime { get; set; }
	
	public string? Note { get; set; }
	
	public int NumberOfCards { get; set; }

	public Guid? CreatedById { get; set; }
	public UserListModel? CreatedBy { get; set; }
	
	public Guid? GroupId { get; set; }
	public GroupListModel? Group { get; set; }

	public ICollection<TagListModel> Tags { get; set; } = new List<TagListModel>();
}