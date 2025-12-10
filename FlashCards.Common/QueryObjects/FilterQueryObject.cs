using FlashCards.Common.QueryObjects.Interfaces;

namespace FlashCards.Common.QueryObjects;

public class FilterQueryObject : IQueryObject
{
    public Guid CreatedByIdFilter { get; set; }
    
    public bool? IsActive { get; set; }
    
    public bool IsDescending { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}