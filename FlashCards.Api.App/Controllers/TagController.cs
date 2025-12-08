using System.Linq.Expressions;
using FlashCards.Api.App.Controllers;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FlashCards.Api.Dal.Entities.InterfacesOrAbstracts;
using FlashCards.Common.QueryObjects;

namespace FlashCards.Api.App.Controllers;

[ApiController]
[Route("api/Tag")]
public class TagController(ITagFacade facade, IUserFacade userFacade) 
    : ControllerBase<TagEntity, TagQueryObject, TagListModel, TagDetailModel>(facade,  userFacade)
{
    
}