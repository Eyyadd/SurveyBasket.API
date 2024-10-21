namespace SurveyBasket.API.DTOs.Polls
{
    public class CreatePollsRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public bool IsPublished { get; set; } = default;
        public DateOnly StartsAt { get; set; }
        public DateOnly EndsAt { get; set; }
    }

}
