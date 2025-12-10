using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Enums;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;

namespace FlashCards.Api.Bl.Facades.Interfaces;

public interface ICardFacade : IFacade<CardEntity, CardQueryObject, CardListModel, CardDetailModel>
{
    public Task<bool?> EnterAnswer(Guid cardId, Guid collectionId, Guid userId, EnumAnswerType answerType);
}