namespace SurveyBasket.API.IRepositories
{
    public interface IPollsRepo : IGenericRepository<Poll>
    {
        Task<bool> ToggledIsPublishedAsync(Poll ToggleIsPublish, CancellationToken cancellationToken);
        void DetachedEntity(Poll PollEntity, CancellationToken cancellationToken);

        bool IsTitleUnique(string title);
    }
}
