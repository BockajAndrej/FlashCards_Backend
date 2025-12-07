using FlashCards.Common.QueryObjects.Interfaces;

namespace FlashCards.Common.QueryObjects;

public class RecordQueryObject  : IQueryObject
{
    public bool IsDescending { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}