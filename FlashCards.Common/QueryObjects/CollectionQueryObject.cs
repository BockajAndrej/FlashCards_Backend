using FlashCards.Common.QueryObjects.Interfaces;

namespace FlashCards.Common.QueryObjects;

public class CollectionQueryObject : IQueryObject
{
    public Guid CreatedByIdFilter { get; set; }
    public string? NameFilter { get; set; }
    
    public ICollection<Guid>? TagIdsFilter { get; set; }
    
    
    public bool? RecentOrder { get; set; }
    public bool? NameOrder { get; set; }
    public bool IsDescending { get; set; }
    
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}