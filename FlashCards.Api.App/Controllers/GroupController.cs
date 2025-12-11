using System.Security.Claims;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController(IGroupFacade facade, IUserFacade userFacade)
        : ControllerBase<GroupEntity, GroupQueryObject, GroupListModel, GroupDetailModel>(facade,  userFacade)
    {
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<GroupListModel>>> Get([FromQuery] GroupQueryObject queryObject)
        {
            var userId = await GetUserId();
            
            var result = await facade.GetAsync(queryObject);
            return Ok(result.ToList());
        }
        
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(Guid id, GroupDetailModel model)
        {
            if (id != model.Id)
                return BadRequest();

            var result = await facade.SaveAsync(model);

            return Ok(result);
        }
        
        [Authorize]
        [HttpPost]
        public override async Task<ActionResult<GroupDetailModel>> Post(GroupDetailModel model)
        {
            model.Id = Guid.Empty;

            var userId = await GetUserId();
            var userListModel = model.Users.FirstOrDefault(l => l.Id == userId);
            if (userListModel != null) 
                userListModel.Role = EnumUserRole.Admin;

            var result = await facade.SaveAsync(model);
            return Ok(result);
        }
    }
}