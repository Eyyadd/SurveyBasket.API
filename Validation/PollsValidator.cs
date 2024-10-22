
using SurveyBasket.API.DTOs.Polls;

namespace SurveyBasket.API.Validation
{
    public class PollsValidator : AbstractValidator<CreatePollsRequest>
    {
        private readonly IPollService _pollService;

        public PollsValidator(IPollService pollService)
        {
            _pollService = pollService;

            RuleFor(P => P.Title)
                .MaximumLength(100);
            RuleFor(P => P.Title)
                .Must(BeUnique)
                .WithMessage("{PropertyName} must be unique");
            RuleFor(P=>P.Summary)
                .MaximumLength(1500);
            RuleFor(P => P.EndsAt)
                .GreaterThanOrEqualTo(P => P.StartsAt)
                .WithName("Ends At")
                .WithMessage("the {PropertyName} Should be Greater Than StartAt");
            
        }
        ///TODO Inject the service poll to check that the title is unique
        private bool BeUnique(string Title)
        {
            var checkUniquness =  _pollService.TitleIsUnique(Title);
            return !checkUniquness;
        }

    }
}
