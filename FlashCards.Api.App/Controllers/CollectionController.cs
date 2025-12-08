using System.Linq.Expressions;
using System.Security.Claims;
using FlashCards.Api.Bl.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;

namespace FlashCards.Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController(ICollectionFacade facade, IUserFacade userFacade)
        : ControllerBase<CollectionEntity, CollectionQueryObject, CollectionListModel, CollectionDetailModel>(facade, userFacade)
    {
        public override async Task<ActionResult<IEnumerable<CollectionListModel>>> Get(
            [FromQuery] CollectionQueryObject queryObject)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return BadRequest();
            var userModel = await userFacade.GetLocalUserAsync(userIdString);
            if (userModel == null)
                return BadRequest();

            queryObject.CreatedById = userModel.Id;

            var result = await facade.GetAsync(queryObject);
            return Ok(result.ToList());
        }

        [HttpPut("{id}")]
        [Authorize]
        public override async Task<IActionResult> Put(Guid id, CollectionDetailModel model)
        {
            if (id != model.Id)
                return BadRequest();

            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return BadRequest();
            var userModel = await userFacade.GetLocalUserAsync(userIdString);
            if (userModel == null)
                return BadRequest();

            model.CreatedById = userModel.Id;
            var result = await facade.SaveAsync(model);

            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public override async Task<ActionResult<CollectionDetailModel>> Post(
            CollectionDetailModel model)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdString == null)
                return BadRequest();
            var userModel = await userFacade.GetLocalUserAsync(userIdString);
            if (userModel == null)
                return BadRequest();

            model.CreatedById = userModel.Id;
            model.Id = Guid.Empty;

            var result = await facade.SaveAsync(model);
            return Ok(result);
        }
    }
}