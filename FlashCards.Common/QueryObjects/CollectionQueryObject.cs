using FlashCards.Common.QueryObjects.Interfaces;

namespace FlashCards.Common.QueryObjects;

public class CollectionQueryObject : IQueryObject
{
    public string? Name { get; set; }
    
    public ICollection<Guid>? TagIds { get; set; }
    
    public bool IsDescending { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}