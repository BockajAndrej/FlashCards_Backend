using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;

namespace FlashCards.Api.App.Controllers;

public class FilterController(IFilterFacade facade) 
    : ControllerBase<FilterEntity, FilterQueryObject, FilterListModel, FilterDetailModel>(facade)
{
    
}