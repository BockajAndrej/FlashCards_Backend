using System.Security.Claims;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController(IFilterFacade facade, IUserFacade userFacade)
        : ControllerBase<FilterEntity, FilterQueryObject, FilterListModel, FilterDetailModel>(facade,  userFacade)
    {
        [HttpGet]
        public override async Task<ActionResult<IEnumerable<FilterListModel>>> Get([FromQuery] FilterQueryObject queryObject)
        {
            var userId = await GetUserId();
            if(userId != null)
                queryObject.CreatedById = (Guid)userId;
            
            var result = await facade.GetAsync(queryObject);
            return Ok(result.ToList());
        }
        
        [HttpPut("{id}")]
        public override async Task<IActionResult> Put(Guid id, FilterDetailModel model)
        {
            if (id != model.Id)
                return BadRequest();

            model.IsActive = true;
            var result = await facade.SaveAsync(model);

            return Ok(result);
        }
        
        [HttpPost]
        public override async Task<ActionResult<FilterDetailModel>> Post(FilterDetailModel model)
        {
            model.Id = Guid.Empty;
            model.IsActive = true;

            var userId = await GetUserId();
            if(userId != null)
                model.UserId = (Guid)userId;
            
            var result = await facade.SaveAsync(model);
            return Ok(result);
        }
    }
}