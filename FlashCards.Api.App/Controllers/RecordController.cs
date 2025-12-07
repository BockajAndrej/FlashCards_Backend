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
	public class RecordController(ICompletedLessonFacade facade, IUserFacade userFacade)
		: ControllerBase<RecordEntity, RecordQueryObject, RecordListModel, RecordDetailModel>(facade)
	{
		[HttpGet("collection/{collectionId:guid}/last")]
		[Authorize]
		public async Task<ActionResult<RecordListModel>> GetLastForCollection(Guid collectionId)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userId))
				return Unauthorized();

			var lastLesson = await facade.GetLastRecordByCollectionIdAsync(collectionId, userId);

			if (lastLesson == null)
				return NotFound();

			return Ok(lastLesson);
		}

		[HttpPost]
		[Authorize]
		public override async Task<ActionResult<RecordDetailModel>> Post(
			RecordDetailModel model)
		{
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userIdString == null)
				return BadRequest();
			var userModel = await userFacade.GetLocalUserAsync(userIdString);
			if (userModel == null)
				return BadRequest();

			model.UserId = userModel.Id;
			model.Id = Guid.Empty;
			model.CreatedDateTime = DateTime.Now;
			
			var result = await facade.SaveAsync(model);
			return Ok(result);
		}
	}
}