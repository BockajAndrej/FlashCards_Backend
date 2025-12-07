namespace FlashCards.Common.QueryObjects.Interfaces;

public interface IQueryObject
{
    public bool IsDescending { get; set; }

    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}