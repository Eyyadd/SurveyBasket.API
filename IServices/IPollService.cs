using SurveyBasket.API.ErrorsHandling;

namespace SurveyBasket.API.IServices
{
    public interface IPollService
    {
        Task<Result<IReadOnlyList<PollsResponse>>> GetAsync(CancellationToken cancellationToken);
        Task<Result<PollsResponse>> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<Result<PollsResponse>> CreateAsync(CreatePollsRequest poll, CancellationToken cancellationToken);
        Task<Result<PollsResponse>> UpdateAsync(int id,CreatePollsRequest poll, CancellationToken cancellationToken);
        Task<Result> DeleteAsync(int id, CancellationToken cancellationToken);

        Task<Result> ToggleIsPublishedAsync(int id, CancellationToken cancellationToken);
        Result TitleIsUnique(string title);

    }
}
