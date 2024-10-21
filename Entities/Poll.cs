namespace SurveyBasket.API.Models
{
    public class Poll
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public bool IsPublished { get; set; }
        public DateOnly StartsAt { get; set; }
        public DateOnly EndsAt { get; set; }

        public Poll() { }

        public Poll(int id, string title, string summary, bool isPublished, DateOnly startsAt, DateOnly endsAt)
        {
            Id = id;
            Title = title;
            Summary = summary;
            IsPublished = isPublished;
            StartsAt = startsAt;
            EndsAt = endsAt;
        }
    }
}
