using System.Linq.Expressions;
using System.Security.Claims;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController(ICardFacade facade) : ControllerBase<CardEntity, CardListModel, CardDetailModel>(facade)
    {
        
        protected override Expression<Func<CardEntity, bool>> CreateFilter(string? strFilterAtrib, string? strFilter)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            Expression<Func<CardEntity, bool>> filter = l => l.CardCollection.UserId == userId;
            if (!string.IsNullOrEmpty(strFilter) && !string.IsNullOrEmpty(strFilterAtrib) && strFilterAtrib.Split(',').Length == strFilter.Split(',').Length)
            {
                for (int i = 0; i < strFilterAtrib.Split(',').Length; i++)
                {
                    var atrib = strFilterAtrib.Split(',')[i];
                    var str = strFilter.Split(',')[i];
                    switch (atrib)
                    {
                        case nameof(CardEntity.Question):
                            filter = ExpressionAnd(filter, l => l.Question.ToLower().Contains(str.ToLower()));
                            break;
                        case nameof(CardEntity.Description):
                            filter = ExpressionAnd(filter, l => l.Description!.ToLower().Contains(str.ToLower()));
                            break;
                        case nameof(CardEntity.CardCollectionId):
                            filter = ExpressionAnd(filter, l => l.CardCollectionId.ToString() == str);
                            break;
                    }
                }
            }
            return filter;
        }

        protected override Func<IQueryable<CardEntity>, IOrderedQueryable<CardEntity>> CreateOrderBy(string? strSortBy, bool sortDesc)
        {
            sortDesc = !sortDesc;
            Func<IQueryable<CardEntity>, IOrderedQueryable<CardEntity>> orderBy = l => l.OrderBy(s => s.Id);
            switch (strSortBy)
            {
                case nameof(CardEntity.Question):
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.Question) 
                        : l => l.OrderByDescending(s => s.Question);
                    break;
                case nameof(CardEntity.Description):
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.Description) 
                        : l => l.OrderByDescending(s => s.Description);
                    break;
                case nameof(CardEntity.LastModifiedDateTime):
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.LastModifiedDateTime) 
                        : l => l.OrderByDescending(s => s.LastModifiedDateTime);
                    break;
            }
            return orderBy;
        }
        
        // [Authorize(Policy = "AdminRole")]
        [Authorize]
        public override async Task<ActionResult<CardDetailModel>> PostCardEntity(CardDetailModel model)
        {
            model.LastModifiedDateTime = DateTime.Now;
            model.Id = Guid.Empty;
            var result = await facade.SaveAsync(model);
            return Ok(result);
        }
    }
}
