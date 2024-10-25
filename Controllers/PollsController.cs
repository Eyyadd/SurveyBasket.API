using Microsoft.AspNetCore.Authorization;
using SurveyBasket.API.Helper.Extensions;

namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PollsController : ControllerBase
    {
        private readonly IPollService _pollService;

        public PollsController(IPollService pollService)
        {
            _pollService = pollService;
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Polls(CancellationToken cancellationToken)
        {
            var Polls = await _pollService.GetAsync(cancellationToken);

            return Polls.IsSucess ?
                Ok(Polls.Value) :
                Polls.Problems();
        }

        [HttpGet("GetOne/{id}")]
        public async Task<IActionResult> Polls([FromRoute] int id, CancellationToken cancellationToken)
        {
            var Poll = await _pollService.GetByIdAsync(id, cancellationToken);
            return Poll.IsSucess ?
                Ok(Poll.Value) :
                Poll.Problems();

        }

        [HttpPost("Create")]
        public async Task<IActionResult> Polls([FromBody] CreatePollsRequest newPoll, CancellationToken cancellationToken)
        {

            var AddedPoll = await _pollService.CreateAsync(newPoll, cancellationToken);
            return AddedPoll.IsSucess ?
                Ok(AddedPoll.Value) :
                AddedPoll.Problems();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> PollAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var DeletedPoll = await _pollService.DeleteAsync(id, cancellationToken);
            return DeletedPoll.IsSucess ?
                Ok("Successfully Deleted") :
                DeletedPoll.Problems();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> PollsAsync([FromRoute] int id, [FromBody] CreatePollsRequest NewPoll, CancellationToken cancellationToken)
        {
            var UpdatedPoll = await _pollService.UpdateAsync(id, NewPoll, cancellationToken);
            return UpdatedPoll.IsSucess ?
                Ok(UpdatedPoll.Value) :
                UpdatedPoll.Problems();
        }

        [HttpGet("{id}/TogglePublish")]
        public async Task<IActionResult> TogglePublished([FromRoute] int id, CancellationToken cancellationToken)
        {
            var Toggled = await _pollService.ToggleIsPublishedAsync(id, cancellationToken);
            return Toggled.IsSucess ? 
                Ok("Toggled Status Sccessfully"):
                Toggled.Problems(); 
        }
    }
}
