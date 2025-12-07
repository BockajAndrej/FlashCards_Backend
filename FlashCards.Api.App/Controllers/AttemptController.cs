using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttemptController(IAttemptFacade facade) : ControllerBase<AttemptEntity, AttemptQueryObject, AttemptListModel, AttemptDetailModel>(facade)
{
    
}