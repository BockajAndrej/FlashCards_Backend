using System.Linq.Expressions;
using System.Security.Claims;
using FlashCards.Api.Bl.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using Microsoft.AspNetCore.Authorization;

namespace FlashCards.Api.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CardCollectionController(ICardCollectionFacade facade) : ControllerBase<CardCollectionEntity, CardCollectionListModel, CardCollectionDetailModel>(facade)
	{
		protected override Expression<Func<CardCollectionEntity, bool>> CreateFilter(string? strFilterAtrib, string? strFilter)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Expression<Func<CardCollectionEntity, bool>>? filter = l => l.UserId == userId;
			if (!string.IsNullOrEmpty(strFilter))
			{
				switch (strFilterAtrib)
				{
					case nameof(CardCollectionEntity.Title):
						filter = l => l.Title.ToLower().Contains(strFilter.ToLower()) && l.UserId == userId;
						break;
				}
			}

			return filter;
		}

		protected override Func<IQueryable<CardCollectionEntity>, IOrderedQueryable<CardCollectionEntity>> CreateOrderBy(string? strSortBy, bool sortDesc)
		{
			sortDesc = !sortDesc;
			Func<IQueryable<CardCollectionEntity>, IOrderedQueryable<CardCollectionEntity>> orderBy = l => l.OrderBy(s => s.Id);
			switch (strSortBy)
			{
				case nameof(CardCollectionEntity.Title):
					orderBy = sortDesc
						? l => l.OrderByDescending(s => s.Title)
						: l => l.OrderBy(s => s.Title);
					break;
				case nameof(CardCollectionEntity.LastModifiedDateTime):
					orderBy = sortDesc
						? l => l.OrderBy(s => s.LastModifiedDateTime)
						: l => l.OrderByDescending(s => s.LastModifiedDateTime);
					break;
				case nameof(CardCollectionEntity.LastPlayedDateTime):
					orderBy = sortDesc
						? l => l
							.OrderByDescending(s => s.LastPlayedDateTime.HasValue)
							.ThenByDescending(s => s.LastPlayedDateTime)
						: l => l
							.OrderByDescending(s => s.LastPlayedDateTime.HasValue)
							.ThenBy(s => s.LastPlayedDateTime);
					break;
			}
			return orderBy;
		}
		
		[Authorize]
		public override async Task<ActionResult<CardCollectionDetailModel>> PostCardEntity(CardCollectionDetailModel model)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			model.UserId = userId ?? throw new UnauthorizedAccessException();
			model.LastModifiedDateTime = DateTime.Now;
			model.Id = Guid.Empty;
			var result = await facade.SaveAsync(model);
			return Ok(result);
		}
	}
}