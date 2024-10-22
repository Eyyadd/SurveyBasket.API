namespace SurveyBasket.API.Repositories
{
    public class PollsRepo : GenericRepository<Poll>, IPollsRepo
    {
        public PollsRepo(SurveyBasketDbContext surveyBasketDbContext) : base(surveyBasketDbContext)
        {
        }

        public  void DetachedEntity(Poll PollEntity, CancellationToken cancellationToken)
        => _surveyBasketDbContext.Entry(PollEntity).State = EntityState.Detached;

        public bool IsTitleUnique(string title)
        {
            var CheckUniquness = _set.Any(x => x.Title == title);
            return CheckUniquness;
        }

        public async Task<bool> ToggledIsPublishedAsync(Poll ToggleIsPublish, CancellationToken cancellationToken = default)
        {
            ToggleIsPublish.IsPublished = !ToggleIsPublish.IsPublished;
            var Toggled = await _surveyBasketDbContext.SaveChangesAsync(cancellationToken);
            return Toggled > 0 ? true : false;
        }
    }
}
