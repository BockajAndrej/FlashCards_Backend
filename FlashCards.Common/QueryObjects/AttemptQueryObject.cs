using FlashCards.Common.QueryObjects.Interfaces;

namespace FlashCards.Common.QueryObjects;

public class AttemptQueryObject :  IQueryObject
{
    public bool IsDescending { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}