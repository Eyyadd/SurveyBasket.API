using SurveyBasket.API.DTOs.Polls;

namespace SurveyBasket.API.IServices
{
    public interface IPollService
    {
        Task<IReadOnlyList<PollsResponse>> GetAsync(CancellationToken cancellationToken);
        Task<PollsResponse?> GetByIdAsync(int id, CancellationToken cancellationToken);

        Task<PollsResponse?> CreateAsync(CreatePollsRequest poll, CancellationToken cancellationToken);
        Task<PollsResponse?> Update(int id,CreatePollsRequest poll, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}
