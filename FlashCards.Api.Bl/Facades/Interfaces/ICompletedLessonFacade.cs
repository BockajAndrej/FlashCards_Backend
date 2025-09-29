using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Facades.Interfaces;

public interface ICompletedLessonFacade : IFacade<CompletedLessonEntity, CompletedLessonListModel, CompletedLessonDetailModel>
{
    
}