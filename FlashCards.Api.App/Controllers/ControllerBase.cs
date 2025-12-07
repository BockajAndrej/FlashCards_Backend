using System.Linq.Expressions;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers;

public abstract class ControllerBase<TEntity, TQueryObject, TListModel, TDetailModel>(
    IFacade<TEntity, TQueryObject, TListModel, TDetailModel> facade) : Controller where TDetailModel : IModel
{
    [HttpGet]
    public virtual async Task<ActionResult<IEnumerable<TListModel>>> Get([FromQuery] TQueryObject queryObject)
    {
        var result = await facade.GetAsync(queryObject);
        return Ok(result.ToList());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TDetailModel>> Get(Guid id)
    {
        var cardEntity = await facade.GetByIdAsync(id);
        return Ok(cardEntity);
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Put(Guid id, TDetailModel model)
    {
        if (id != model.Id)
            return BadRequest();

        var result = await facade.SaveAsync(model);

        return Ok(result);
    }

    [HttpPost]
    public virtual async Task<ActionResult<TDetailModel>> Post(TDetailModel model)
    {
        model.Id = Guid.Empty;
        var result = await facade.SaveAsync(model);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await facade.DeleteAsync(id);
        if (result != null)
            return NoContent();
        return NotFound();
    }

    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCountAsync([FromQuery] TQueryObject queryObject)
    {
        var result = await facade.GetAsync(queryObject);
        return result.ToList().Count();
    }
}