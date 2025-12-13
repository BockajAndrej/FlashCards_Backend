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
    [HttpGet]
    public override async Task<ActionResult<IEnumerable<TagListModel>>> Get([FromQuery] TagQueryObject queryObject)
    {
        var user = await GetLocalUser();
        if (user != null)
            queryObject.CreatedByIdFilter = user.Id;
        else
            return BadRequest();
            
        var result = await facade.GetAsync(queryObject);
        return Ok(result.ToList());
    }
        
    [HttpPost]
    public override async Task<ActionResult<TagDetailModel>> Post(TagDetailModel model)
    {
        model.Id = Guid.Empty;
        var user = await GetLocalUser();
        if (user != null)
            model.UserId = user.Id;
        else
            return BadRequest();
            
        var result = await facade.SaveAsync(model);
        return Ok(result);
    }
}