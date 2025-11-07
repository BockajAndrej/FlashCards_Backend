using System.Linq.Expressions;
using System.Security.Claims;
using FlashCards.Api.Bl.Facades.Interfaces;
using Microsoft.AspNetCore.Mvc;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Client;

namespace FlashCards.Api.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CardCollectionController(ICardCollectionFacade facade, IUserFacade userFacade)
		: ControllerBase<CardCollectionEntity, CardCollectionListModel, CardCollectionDetailModel>(facade)
	{
		protected override Expression<Func<CardCollectionEntity, bool>> CreateFilter(string? strFilterAtrib,
			string? strFilter)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			Expression<Func<CardCollectionEntity, bool>>? filter = l => l.CreatedBy.RealUserUrl == userId;

			if (!string.IsNullOrEmpty(strFilter) && !string.IsNullOrEmpty(strFilterAtrib) &&
			    strFilterAtrib.Split(FilterArgumentSpitChar).Length == strFilter.Split(FilterArgumentSpitChar).Length)
			{
				for (int i = 0; i < strFilterAtrib.Split(FilterArgumentSpitChar).Length; i++)
				{
					var atrib = strFilterAtrib.Split(FilterArgumentSpitChar)[i];
					var str = strFilter.Split(FilterArgumentSpitChar)[i];
					switch (atrib)
					{
						case nameof(CardCollectionEntity.Title):
							filter = ExpressionAnd(filter, l => l.Title.ToLower().Contains(strFilter.ToLower()));
							break;
						case "TagId": 
							if (Guid.TryParse(str, out Guid tagId))
							{
								filter = ExpressionAnd(filter, l => l.Tags.Any(t => t.Id == tagId));
							}

							break;
					}
				}
			}

			return filter;
		}

		protected override Func<IQueryable<CardCollectionEntity>, IOrderedQueryable<CardCollectionEntity>>
			CreateOrderBy(string? strSortBy, bool sortDesc)
		{
			Func<IQueryable<CardCollectionEntity>, IOrderedQueryable<CardCollectionEntity>> orderBy = l =>
				l.OrderBy(s => s.Id);
			switch (strSortBy)
			{
				case nameof(CardCollectionEntity.Title):
					orderBy = sortDesc
						? l => l.OrderBy(s => s.Title)
						: l => l.OrderByDescending(s => s.Title);
					break;
			}

			return orderBy;
		}

		[HttpPost]
		[Authorize]
		public override async Task<ActionResult<CardCollectionDetailModel>> Post(
			CardCollectionDetailModel model)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var userModel = await userFacade.GetAsync(l => l.RealUserUrl == userIdString);

			if (!userModel.Any())
				return BadRequest();

			model.CreatedById = userModel.FirstOrDefault()!.Id;
			model.Id = Guid.Empty;

			var result = await facade.SaveAsync(model);
			return Ok(result);
		}
	}
}