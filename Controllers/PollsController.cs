
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
        public IActionResult Polls()
        {
            return Ok(_pollService.GetPolls());
        }

        [HttpGet("GetOne/{id}")]
        public IActionResult Polls([FromRoute] int id)
        {
            return Ok(_pollService.GetPollById(id));
        }

        [HttpPost("Add")]
        public IActionResult Polls([FromBody] Poll newPoll)
        {
            var AddPoll = _pollService.AddPoll(newPoll);
            return Ok(AddPoll);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Poll([FromRoute] int id)
        {
            _pollService.DeletePoll(id);
            return Ok();
        }

        [HttpPut("Update/{id}")]
        public IActionResult Polls([FromRoute]int id,[FromBody] Poll newPoll)
        {
            var AddPoll = _pollService.UpdatePoll(newPoll);
            return Ok(AddPoll);
        }
    }
}
