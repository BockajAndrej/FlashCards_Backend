using System.Linq.Expressions;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Common.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers;

public abstract class ControllerBase<TEntity, TListModel, TDetailModel>(
    IFacade<TEntity, TListModel, TDetailModel> facade) : Controller where TDetailModel : IEntityModel
{
    protected abstract Expression<Func<TEntity, bool>> CreateFilter(string? strFilterAtrib, string? strFilter);

    protected abstract Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> CreateOrderBy(string? strSortBy,
        bool sortDesc);

    protected Expression<Func<T, bool>> ExpressionAnd<T>(Expression<Func<T, bool>> left,
        Expression<Func<T, bool>> right)
    {
        var invokedExpr = Expression.Invoke(right, left.Parameters.Cast<Expression>());
        return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(left.Body, invokedExpr), left.Parameters);
    }

    // GET: api/Card
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TListModel>>> GetCard(
        [FromQuery] string? strFilterAtrib,
        [FromQuery] string? strFilter,
        [FromQuery] string? strSortBy,
        [FromQuery] bool sortDesc = false,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var filter = CreateFilter(strFilterAtrib, strFilter);

        var orderBy = CreateOrderBy(strSortBy, sortDesc);

        var result = await facade.GetAsync(filter, orderBy, pageNumber, pageSize);
        return Ok(result.ToList());
    }

    // GET: api/Card/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TDetailModel>> GetCardEntity(Guid id)
    {
        var cardEntity = await facade.GetByIdAsync(id);
        return Ok(cardEntity);
    }

    // PUT: api/Card/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCardEntity(Guid id, TDetailModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        await facade.SaveAsync(model);

        return NoContent();
    }

    // POST: api/Card
    [HttpPost]
    public virtual async Task<ActionResult<TDetailModel>> PostCardEntity(TDetailModel model)
    {
        model.Id = Guid.Empty;
        var result = await facade.SaveAsync(model);
        return Ok(result);
    }

    // DELETE: api/Card/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCardEntity(Guid id)
    {
        var result = await facade.DeleteAsync(id);
        if (result)
            return NoContent();
        return NotFound();
    }
    
    [HttpGet("count")]
    public async Task<ActionResult<int>> GetCardCollectionCountAsync([FromQuery] string? strFilterAtrib, [FromQuery] string? strFilter)
    {
        var result = await facade.GetAsync(filter: CreateFilter(strFilterAtrib, strFilter), pageSize: 1000);
        return result.ToList().Count();
    }
}