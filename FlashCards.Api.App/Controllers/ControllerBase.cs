using System.Linq.Expressions;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers;

public abstract class ControllerBase<TEntity, TQueryObject, TListModel, TDetailModel>(IFacade<TEntity, TQueryObject, TListModel, TDetailModel> facade) : Controller where TDetailModel : IModel
{
    // GET: api/Card
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TListModel>>> Get(TQueryObject queryObject)
    {
        var result = await facade.GetAsync(queryObject);
        return Ok(result.ToList());
    }

        // GET: api/Card/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TDetailModel>> Get(Guid id)
        {
            var cardEntity = await facade.GetByIdAsync(id);
            return Ok(cardEntity);
        }

        // PUT: api/Card/5
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put(Guid id, TDetailModel model)
        {
            if (id != model.Id)
                return BadRequest();
            
            var result = await facade.SaveAsync(model);
            
            return Ok(result);
        }

        // POST: api/Card
        [HttpPost]
        public virtual async Task<ActionResult<TDetailModel>> Post(TDetailModel model)
        {
            model.Id = Guid.Empty;
            var result = await facade.SaveAsync(model);
            return Ok(result);
        }

        // DELETE: api/Card/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await facade.DeleteAsync(id);
            if(result != null)
                return NoContent();
            return NotFound();
        }
        
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetCountAsync(TQueryObject queryObject)
        {
            var result = await facade.GetAsync(queryObject);
            return result.ToList().Count();
        }
    
}