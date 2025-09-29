using AutoMapper;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;

namespace FlashCards.Api.Bl.Facades;

public class CardCollectionFacade(FlashCardsDbContext dbContext, IMapper mapper) 
    : FacadeBase<CardCollectionEntity, CardCollectionListModel, CardCollectionDetailModel>(dbContext, mapper), 
        ICardCollectionFacade
{
    
}