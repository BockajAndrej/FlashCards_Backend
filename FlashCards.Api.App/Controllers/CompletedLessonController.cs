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
	public class CompletedLessonController(ICompletedLessonFacade facade, IUserFacade userFacade)
		: ControllerBase<CompletedLessonEntity, CompletedLessonListModel, CompletedLessonDetailModel>(facade)
	{
		protected override Expression<Func<CompletedLessonEntity, bool>> CreateFilter(string? strFilterAtrib,
			string? strFilter)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Expression<Func<CompletedLessonEntity, bool>> filter = l => l.User.RealUserUrl == userId;

			if (!string.IsNullOrEmpty(strFilter) && !string.IsNullOrEmpty(strFilterAtrib) &&
			    strFilterAtrib.Split(FilterArgumentSpitChar).Length == strFilter.Split(FilterArgumentSpitChar).Length)
			{
				for (int i = 0; i < strFilterAtrib.Split(FilterArgumentSpitChar).Length; i++)
				{
					var atrib = strFilterAtrib.Split(FilterArgumentSpitChar)[i];
					var str = strFilter.Split(FilterArgumentSpitChar)[i];
					switch (atrib)
					{
						case nameof(CompletedLessonEntity.CardCollection):
							filter = ExpressionAnd(filter,
								l => l.CardCollection != null &&
								     l.CardCollection.Title.ToLower().Contains(str.ToLower()));
							break;

						case nameof(CompletedLessonEntity.CardCollectionId):
							if (Guid.TryParse(str, out var collectionId))
							{
								filter = ExpressionAnd(filter, l => l.CardCollectionId == collectionId);
							}

							break;
					}
				}
			}

			return filter;
		}

		protected override Func<IQueryable<CompletedLessonEntity>, IOrderedQueryable<CompletedLessonEntity>>
			CreateOrderBy(string? strSortBy, bool sortDesc)
		{
			if (string.IsNullOrEmpty(strSortBy))
			{
				return q => q.OrderByDescending(e => e.CreatedDateTime);
			}

			switch (strSortBy)
			{
				case nameof(CompletedLessonEntity.CreatedDateTime):
					return sortDesc
						? q => q.OrderByDescending(e => e.CreatedDateTime)
						: q => q.OrderBy(e => e.CreatedDateTime);

				case nameof(CompletedLessonEntity.CardCollection.Title):
					return sortDesc
						? q => q.OrderByDescending(s => s.CardCollection.Title)
						: q => q.OrderBy(s => s.CardCollection.Title);

				default:
					return q => q.OrderBy(s => s.Id);
			}
		}

		[HttpGet("collection/{collectionId:guid}/last")]
		[Authorize]
		public async Task<ActionResult<CompletedLessonListModel>> GetLastForCollection(Guid collectionId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userId))
			{
				return Unauthorized();
			}

			var lastLesson = await facade.GetLastLessonByCollectionIdAsync(collectionId, userId);

			if (lastLesson == null)
			{
				return NotFound();
			}

			return Ok(lastLesson);
		}

		[HttpPost]
		[Authorize]
		public override async Task<ActionResult<CompletedLessonDetailModel>> Post(
			CompletedLessonDetailModel model)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var userModel = await userFacade.GetAsync(l => l.RealUserUrl == userIdString);

			if (!userModel.Any())
				return BadRequest();

			model.UserId = userModel.FirstOrDefault()!.Id;
			model.Id = Guid.Empty;
			var result = await facade.SaveAsync(model);
			return Ok(result);
		}
	}
}