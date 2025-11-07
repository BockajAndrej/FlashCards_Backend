using FlashCards.Api.Bl.Facades.Interfaces;
using FlashCards.Common.Models.Create;
using Microsoft.AspNetCore.Mvc;

namespace FlashCards.Api.App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonAttemptController : ControllerBase
{
    private readonly ILessonAttemptFacade _lessonAttemptFacade;

    public LessonAttemptController(ILessonAttemptFacade lessonAttemptFacade)
    {
        _lessonAttemptFacade = lessonAttemptFacade;
    }

    [HttpPost]
    public async Task<ActionResult<LessonAttemptCreateModel>> Post(LessonAttemptCreateModel model)
    {
        if (model.Id != Guid.Empty)
        {
            return BadRequest("ID has to be empty for new record.");
        }
        
        var result = await _lessonAttemptFacade.LogAttemptAsync(model);
        return Ok(result);
    }
}