using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Facades.Interfaces;

public interface ICardCollectionFacade : IFacade<CardCollectionEntity, CardCollectionListModel, CardCollectionDetailModel>
{
    
}