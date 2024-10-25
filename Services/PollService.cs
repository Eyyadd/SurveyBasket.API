using SurveyBasket.API.ErrorsHandling;
using SurveyBasket.API.ErrorsHandling.CustomErrorMessages;

namespace SurveyBasket.API.Services
{
    public class PollService : IPollService
    {
        private readonly IPollsRepo _pollRepos;
        private readonly IMapper _mapper;

        public PollService(IPollsRepo PollRepos, IMapper mapper)
        {
            _pollRepos = PollRepos;
            _mapper = mapper;
        }

        public async Task<Result<PollsResponse>> CreateAsync(CreatePollsRequest poll, CancellationToken cancellationToken)
        {

            var MappedPoll = _mapper.Map<Poll>(poll);
            var CreatedPoll = await _pollRepos.CreateAsync(MappedPoll, cancellationToken);
            if (CreatedPoll is not null)
                return Result.Success(CreatedPoll.Adapt<PollsResponse>());
            return Result.Failure<PollsResponse>(PollsErrors.PollNotCreated);
        }

        public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var GetPoll = await _pollRepos.GetByIdAsync(id, cancellationToken);
            if (GetPoll is not null)
            {
                var Deleted = await _pollRepos.DeleteAsync(GetPoll, cancellationToken);
                if (Deleted > 0)
                    return Result.Success();

            }
            return Result.Failure(PollsErrors.PollNotFound);
        }

        public async Task<Result<IReadOnlyList<PollsResponse>>> GetAsync(CancellationToken cancellationToken)
        {
            var Polls = await _pollRepos.GetAsync(cancellationToken);
            var MappedPolls = _mapper.Map<IReadOnlyList<PollsResponse>>(Polls);
            return Result.Success(MappedPolls);
        }

        public async Task<Result<PollsResponse>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var GetPoll = await _pollRepos.GetByIdAsync(id, cancellationToken);
            if (GetPoll is not null)
            {
                var MappedPolls = _mapper.Map<PollsResponse>(GetPoll);
                return Result.Success(MappedPolls);
            }
            return Result.Failure<PollsResponse>(PollsErrors.PollNotFound);
        }

        public Result TitleIsUnique(string title)
        {
            var isUnique = _pollRepos.IsTitleUnique(title);
            if (isUnique) return Result.Success();
            return Result.Failure(PollsErrors.PollTitleIsNotUnique);
        }


        public async Task<Result> ToggleIsPublishedAsync(int id, CancellationToken cancellationToken)
        {
            var IsToggled = false;
            var GetPoll = await _pollRepos.GetByIdAsync(id, cancellationToken);
            if (GetPoll is not null)
            {
                IsToggled = await _pollRepos.ToggledIsPublishedAsync(GetPoll, cancellationToken);
                return Result.Success();
            }
            return Result.Failure(PollsErrors.PollIsPublishedNotToggled);
        }

        public async Task<Result<PollsResponse>> UpdateAsync(int id, CreatePollsRequest poll, CancellationToken cancellationToken)
        {
            var GetPoll = await _pollRepos.GetByIdAsync(id, cancellationToken);
            if (GetPoll is not null)
            {
                _pollRepos.DetachedEntity(GetPoll, cancellationToken);
                var mappedPoll = _mapper.Map<Poll>(poll);
                mappedPoll.Id = id;
                var Updated = await _pollRepos.UpdateAsync(mappedPoll, cancellationToken);
                if (Updated > 0)
                {
                    return Result.Success(_mapper.Map<PollsResponse>(mappedPoll));
                }
            }
            return Result.Failure<PollsResponse>(PollsErrors.PollNotFound);
        }


    }
}
