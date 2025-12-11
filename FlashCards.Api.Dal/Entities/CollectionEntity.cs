using System.ComponentModel.DataAnnotations;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.Enums;

namespace FlashCards.Api.Dal.Entities;

public record CollectionEntity : IEntity
{
	public Guid Id { get; set; }
	[Required] public string Title { get; set; } = null!;
	public string? Description { get; set; }
	public DateTime? StartTimeForAcceptedAnswers { get; set; }
	public DateTime? EndTimeForAcceptedAnswers { get; set; }
	[Required] public DateTime LastModifiedDateTime { get; set; }
	public DateTime? LastPlayedDateTime { get; set; }

	public string? Note { get; set; }

	[Required] public Guid CreatedById { get; set; }
	[Required] public UserEntity CreatedBy { get; set; } = null!;

	[Required] public EnumCardVisibilityType Visibility { get; set; }
	
	public Guid? GroupId { get; set; }
	public GroupEntity? Group { get; set; }

	public ICollection<CardEntity> Cards { get; set; } = new List<CardEntity>();
	public ICollection<RecordEntity> Records { get; set; } = new List<RecordEntity>();

	public ICollection<CollectionTagEntity> TagBelong { get; set; } = new List<CollectionTagEntity>();
}