namespace SurveyBasket.API.IServices
{
    public interface IPollService
    {
        Task<IReadOnlyList<PollsResponse>> GetAsync(CancellationToken cancellationToken);
        Task<PollsResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<PollsResponse?> CreateAsync(CreatePollsRequest poll, CancellationToken cancellationToken);
        Task<PollsResponse?> UpdateAsync(int id,CreatePollsRequest poll, CancellationToken cancellationToken);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<bool> ToggleIsPublishedAsync(int id, CancellationToken cancellationToken);
        bool TitleIsUnique(string title);

    }
}
