using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;

namespace FlashCards.Api.Bl.Facades.Interfaces;

public interface
	IRecordFacade : IFacade<RecordEntity, RecordQueryObject, RecordListModel, RecordDetailModel>
{
	Task<RecordDetailModel?> GetLastRecordByCollectionIdAsync(Guid collectionId, Guid userId);
	public Task<RecordDetailModel?> GetActiveRecordByCollectionIdAsync(Guid collectionId, Guid userId);
}