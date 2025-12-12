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
			var user = await GetLocalUser();
			if (user == null)
				return Unauthorized();

			var lastLesson = await facade.GetLastRecordByCollectionIdAsync(collectionId, user.Id);

			if (lastLesson == null)
				return NotFound();

			return Ok(lastLesson);
		}
		
		[HttpGet("collection/{collectionId:guid}/active")]
		[Authorize]
		public async Task<ActionResult<RecordDetailModel>> GetActiveForCollection(Guid collectionId)
		{
			var user = await GetLocalUser();
			if (user == null)
				return Unauthorized();

			var lastLesson = await facade.GetActiveRecordByCollectionIdAsync(collectionId, user.Id);

			if (lastLesson == null)
				return NotFound();

			return Ok(lastLesson);
		}

		[HttpPost("StartNewGame")]
		[Authorize]
		public async Task<ActionResult<RecordDetailModel>> Post(
			RecordDetailModel model)
		{
			var user = await GetLocalUser();
			if (user == null)
				return Unauthorized();

			model.Id = Guid.Empty;
			model.UserId = user.Id;
			
			var result = await facade.SaveAsync(model);
			return Ok(result);
		}
		
		[HttpPut("FinishGame")]
		[Authorize]
		public async Task<ActionResult<RecordDetailModel>> Put(
			RecordDetailModel model)
		{
			var user = await GetLocalUser();
			if (user == null)
				return Unauthorized();
			
			if(model.Id == Guid.Empty || model.CardCollectionId == Guid.Empty)
				return BadRequest();
			
			model.UserId = user.Id;
			model.IsCompleted = true;
			
			var result = await facade.SaveAsync(model);
			return Ok(result);
		}
	}
}