using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Claims;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompletedLessonController(ICompletedLessonFacade facade, ICardCollectionFacade cardCollectionFacade)
        : ControllerBase<CompletedLessonEntity, CompletedLessonListModel, CompletedLessonDetailModel>(facade)
    {
        protected override Expression<Func<CompletedLessonEntity, bool>> CreateFilter(string? strFilterAtrib, string? strFilter)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Expression<Func<CompletedLessonEntity, bool>> filter = l => l.UserId == userId;
            if (!string.IsNullOrEmpty(strFilter))
            {
                if (!string.IsNullOrEmpty(strFilter))
                    filter = l => l.CardCollection.Title.ToLower().Contains(strFilter.ToLower()) && l.UserId == userId;
            }
            return filter;
        }

        protected override Func<IQueryable<CompletedLessonEntity>, IOrderedQueryable<CompletedLessonEntity>> CreateOrderBy(string? strSortBy, bool sortDesc)
        {
            sortDesc = !sortDesc;
            Func<IQueryable<CompletedLessonEntity>, IOrderedQueryable<CompletedLessonEntity>> orderBy = l => l.OrderBy(s => s.Id);
            switch (strSortBy)
            {
                case nameof(CompletedLessonEntity.CardCollection.Title):
                    orderBy = sortDesc 
                        ? l => l.OrderBy(s => s.CardCollection.Title) 
                        : l => l.OrderByDescending(s => s.CardCollection.Title);
                    break;
            }

            return orderBy;
        }
        
        [HttpPost]
        [Authorize]
        public override async Task<ActionResult<CompletedLessonDetailModel>> PostCardEntity(CompletedLessonDetailModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.UserId = userId ?? throw new UnauthorizedAccessException();
            model.Id = Guid.Empty;
            model.CreatedDateTime = DateTime.Now;
            
            var colleciton = await cardCollectionFacade.GetByIdAsync(model.CardCollectionId);
            Debug.Assert(colleciton != null, nameof(colleciton) + " != null");
            colleciton.LastPlayedDateTime = model.CreatedDateTime;
            await cardCollectionFacade.SaveAsync(colleciton);
            
            var result = await facade.SaveAsync(model);
            return Ok(result);
        }
    }
}