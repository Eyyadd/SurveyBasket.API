
using SurveyBasket.API.DTOs.Polls;
using System.Threading;

namespace SurveyBasket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            if (Polls is not null)
                return Ok(Polls);
            return BadRequest();
        }

        [HttpGet("GetOne/{id}")]
        public async Task<IActionResult> Polls([FromRoute] int id, CancellationToken cancellationToken)
        {
            var Poll = await _pollService.GetByIdAsync(id, cancellationToken);
            if (Poll is not null)
                return Ok(Poll);
            return BadRequest();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Polls([FromBody] CreatePollsRequest newPoll, CancellationToken cancellationToken)
        {

            var AddedPoll = await _pollService.CreateAsync(newPoll, cancellationToken);
            if (AddedPoll is not null)
                return Ok(AddedPoll);
            return BadRequest(AddedPoll);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> PollAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var DeletedPoll = await _pollService.DeleteAsync(id, cancellationToken);
            if (DeletedPoll)
                return Ok("Deleted Sucessfully");
            return BadRequest("We can't Delete this Entity");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Polls([FromRoute] int id, [FromBody] CreatePollsRequest NewPoll, CancellationToken cancellationToken)
        {
            var UpdatedPoll = _pollService.UpdateAsync(id, NewPoll, cancellationToken);
            if (UpdatedPoll is not null)
                return Ok(UpdatedPoll);
            return BadRequest(UpdatedPoll);
        }

        //[HttpGet("{id}/TogglePublish")]
        //public async Task<IActionResult> TogglePublished([FromRoute] int id, CancellationToken cancellationToken)
        //{
        //    var Toggled= await _pollService.ToggleIsPublishedAsync(id, cancellationToken);
        //    if(Toggled)
        //        return Ok(Toggled);
        //    return BadRequest(Toggled);
        //}
    }
}
