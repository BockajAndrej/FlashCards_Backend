using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;

namespace FlashCards.Api.Bl.Facades.Interfaces;

public interface
	ICompletedLessonFacade : IFacade<RecordEntity, RecordQueryObject, RecordListModel, RecordDetailModel>
{
	Task<RecordListModel?> GetLastRecordByCollectionIdAsync(Guid collectionId, string userId);
}