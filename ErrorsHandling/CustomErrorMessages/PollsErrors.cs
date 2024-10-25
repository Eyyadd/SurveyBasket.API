namespace SurveyBasket.API.ErrorsHandling.CustomErrorMessages
{
    public static class PollsErrors
    {
        public static readonly Error PollNotFound = new Error("404", "this poll not found", 404);
        public static readonly Error PollTitleIsNotUnique = new Error("404", "this poll should have unique title", 404);
        public static readonly Error PollIsPublishedNotToggled = new Error("404", "this poll status 'Is Published' could not be toggled", 404);
        public static readonly Error PollNotCreated = new Error("400", "this poll can not be Created", 400);
    }
}
