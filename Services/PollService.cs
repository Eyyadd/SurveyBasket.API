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

        public async Task<PollsResponse?> CreateAsync(CreatePollsRequest poll, CancellationToken cancellationToken)
        {

            var MappedPoll = _mapper.Map<Poll>(poll);
            var CreatedPoll = await _pollRepos.CreateAsync(MappedPoll, cancellationToken);
            return CreatedPoll is not null ? _mapper.Map<PollsResponse>(CreatedPoll) : null;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var GetPoll = await _pollRepos.GetByIdAsync(id, cancellationToken);
            if (GetPoll is not null)
            {
                var Deleted = await _pollRepos.DeleteAsync(GetPoll, cancellationToken);
                if (Deleted > 0)
                    return true;

            }
            return false;
        }

        public async Task<IReadOnlyList<PollsResponse>> GetAsync(CancellationToken cancellationToken)
        {
            var Polls = await _pollRepos.GetAsync(cancellationToken);
            var MappedPolls = _mapper.Map<IReadOnlyList<PollsResponse>>(Polls);
            return MappedPolls;
        }

        public async Task<PollsResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var GetPoll = await _pollRepos.GetByIdAsync(id, cancellationToken);
            if (GetPoll is not null)
            {
                var MappedPolls = _mapper.Map<PollsResponse>(GetPoll);
                return MappedPolls;
            }
            return null;
        }

        public bool TitleIsUnique(string title)
        =>  _pollRepos.IsTitleUnique(title);


        public async Task<bool> ToggleIsPublishedAsync(int id, CancellationToken cancellationToken)
        {
            var IsToggled = false;
            var GetPoll = await _pollRepos.GetByIdAsync(id, cancellationToken);
            if (GetPoll is not null)
            {
                IsToggled = await _pollRepos.ToggledIsPublishedAsync(GetPoll, cancellationToken);
            }
            return IsToggled;
        }

        public async Task<PollsResponse?> UpdateAsync(int id, CreatePollsRequest poll, CancellationToken cancellationToken)
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
                    return _mapper.Map<PollsResponse>(mappedPoll);
                }
            }
            return null;
        }
    }
}
