using System.Linq.Expressions;
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
    public class UserController(IUserFacade facade, IUserFacade userFacade)
        : ControllerBase<UserEntity, UserQueryObject, UserListModel, UserDetailModel>(facade, userFacade)
    {
        [HttpGet]
        [Authorize]
        public override async Task<ActionResult<IEnumerable<UserListModel>>> Get(
            [FromQuery] UserQueryObject queryObject)
        {
            var user = await GetLocalUser();
            if (user?.Role == EnumUserRole.Admin)
            {
                var result = await facade.GetAsync(queryObject);
                return Ok(result.ToList());
            }

            return Unauthorized();
        }

        [HttpGet("{id}")]
        [Authorize]
        public override async Task<ActionResult<UserDetailModel>> Get(Guid id)
        {
            var user = await GetLocalUser();
            if (user?.Role == EnumUserRole.Admin)
            {
                var cardEntity = await facade.GetByIdAsync(id);
                return Ok(cardEntity);
            }

            return Unauthorized();
        }

        [HttpGet("GetActualUser")]
        [Authorize]
        public async Task<ActionResult<UserDetailModel>> GetActualUser()
        {
            var useRole = User.FindFirstValue(ClaimTypes.Role);
            if (useRole == null)
                return NotFound();

            var result = await GetLocalUser();
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public override async Task<IActionResult> Put(Guid id, UserDetailModel model)
        {
            if (id != model.Id)
                return BadRequest();

            var user = await GetLocalUser();
            if (user?.Role == EnumUserRole.Admin)
            {
                var result = await facade.SaveAsync(model);

                return Ok(result);
            }

            return Unauthorized();
        }

        [HttpPost]
        [Authorize]
        public override async Task<ActionResult<UserDetailModel>> Post(UserDetailModel model)
        {
            var user = await GetLocalUser();
            if (user?.Role == EnumUserRole.Admin)
            {
                model.Id = Guid.Empty;
                var result = await facade.SaveAsync(model);
                return Ok(result);
            }

            return Unauthorized();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public override async Task<IActionResult> Delete(Guid id)
        {
            var user = await GetLocalUser();
            if (user?.Role == EnumUserRole.Admin)
            {
                var result = await facade.DeleteAsync(id);
                if (result != null)
                    return NoContent();
                return NotFound();
            }
            return Unauthorized();
        }
        
        [HttpPost("CreateUser")]
        [Authorize]
        public async Task<ActionResult<UserDetailModel>> CreateUser(UserDetailModel model)
        {
            var user = await GetLocalUser();
            if (user == null)
            {
                var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userIdString == null)
                    return Unauthorized();
                
                model.Id = Guid.Empty;
                model.RealUserUrl = userIdString;
                model.Role = EnumUserRole.Member;
                
                var result = await facade.SaveAsync(model);
                return Ok(result);
            }
            return BadRequest();
        }
    }
}