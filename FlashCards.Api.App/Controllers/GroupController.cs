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
            var user = await GetLocalUser();
            if (user == null)
                return BadRequest();

            var result = await facade.GetAsync(queryObject, user.Id);
            
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

            var user = await GetLocalUser();
            
            if (user != null)
                user.Role = EnumUserRole.Admin;
            else
                return BadRequest();

            if (model.Users.Any())
                return BadRequest();
            
            model.Users.Add(user);

            var result = await facade.SaveAsync(model);
            return Ok(result);
        }
        
        [HttpPut("JoinToGroup")]
        public async Task<IActionResult> JoinToGroup(GroupDetailModel model)
        {
            var user = await GetLocalUser();
            
            if (user != null)
                user.Role = EnumUserRole.Member;
            else
                return BadRequest();
            
            model.Users.Add(user);

            var result = await facade.SaveAsync(model);

            return Ok(result);
        }
        
        [HttpPut("LeaveFromGroup")]
        public async Task<IActionResult> LeaveFromGroup(GroupDetailModel model)
        {
            var user = await GetLocalUser();
            if (user == null)
                return BadRequest("User not found");

            await facade.RemoveUserFromGroupAsync(model.Id, user.Id);

            return Ok();
        }
    }
}