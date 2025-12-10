using System.Linq.Expressions;
using System.Security.Claims;
using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Api.Dal.Entities;
using FlashCards.Common.Enums;
using FlashCards.Common.Models.Details;
using FlashCards.Common.Models.Lists;
using FlashCards.Common.QueryObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecordController(IRecordFacade facade, IUserFacade userFacade)
		: ControllerBase<RecordEntity, RecordQueryObject, RecordListModel, RecordDetailModel>(facade, userFacade)
	{
		[HttpGet("collection/{collectionId:guid}/last")]
		[Authorize]
		public async Task<ActionResult<RecordDetailModel>> GetLastForCollection(Guid collectionId)
		{
			var userId = await GetUserId();
			if (userId == null)
				return Unauthorized();

			var lastLesson = await facade.GetLastRecordByCollectionIdAsync(collectionId, (Guid)userId);

			if (lastLesson == null)
				return NotFound();

			return Ok(lastLesson);
		}
		
		[HttpGet("collection/{collectionId:guid}/active")]
		[Authorize]
		public async Task<ActionResult<RecordDetailModel>> GetActiveForCollection(Guid collectionId)
		{
			var userId = await GetUserId();
			if (userId == null)
				return Unauthorized();

			var lastLesson = await facade.GetActiveRecordByCollectionIdAsync(collectionId, (Guid)userId);

			if (lastLesson == null)
				return NotFound();

			return Ok(lastLesson);
		}

		[HttpPost("StartNewGame")]
		[Authorize]
		public async Task<ActionResult<RecordDetailModel>> Post(
			RecordDetailModel model, Guid collectionId)
		{
			var userId = await GetUserId();
			if (userId == null)
				return Unauthorized();

			model.Id = Guid.Empty;
			
			model.UserId = (Guid)userId;
			model.CardCollectionId = collectionId;
			
			var result = await facade.SaveAsync(model);
			return Ok(result);
		}
		
		[HttpPut("FinishGame")]
		[Authorize]
		public async Task<ActionResult<RecordDetailModel>> Put(
			RecordDetailModel model, Guid collectionId)
		{
			var userId = await GetUserId();
			if (userId == null)
				return Unauthorized();
			
			if(model.Id == Guid.Empty)
				return BadRequest();
			
			model.UserId = (Guid)userId;
			model.CardCollectionId = collectionId;
			model.IsCompleted = true;
			
			var result = await facade.SaveAsync(model);
			return Ok(result);
		}
	}
}