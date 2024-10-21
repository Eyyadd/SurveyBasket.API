
namespace SurveyBasket.API.Validation
{
    public class PollsValidator : AbstractValidator<Poll>
    {
        public PollsValidator()
        {
            RuleFor(P => P.Title)
                .MaximumLength(100);
            RuleFor(P=>P.Summary)
                .MaximumLength(1500);
            RuleFor(P => P.EndsAt)
                .GreaterThanOrEqualTo(P => P.StartsAt)
                .WithName("Ends At")
                .WithMessage("the {PropertyName} Should be Greater Than StartAt");
                
        }
        ///TODO Inject the service poll to check that the title is unique
        //private bool BeUnique(Poll poll)
        //{

        //}

    }
}
