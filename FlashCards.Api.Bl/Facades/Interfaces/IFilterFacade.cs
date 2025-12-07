using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;

namespace FlashCards.Api.Bl.Facades.Interfaces;

public interface IFilterFacade : IFacade<FilterEntity, FilterQueryObject, FilterListModel, FilterDetailModel>
{
    
}