using SurveyBasket.API.Core;
using SurveyBasket.API.DTOs.Polls;
using SurveyBasket.API.IRepositories;
using System.Threading;

namespace SurveyBasket.API.Services
{
    public class PollService : IPollService
    {
        private readonly IGenericRepository<Poll> _pollRepos;
        private readonly IMapper _mapper;

        public PollService(IGenericRepository<Poll> PollRepos, IMapper mapper)
        {
            _pollRepos = PollRepos;
            _mapper = mapper;
        }

        public async Task<PollsResponse?> CreateAsync(CreatePollsRequest poll, CancellationToken cancellationToken)
        {
            if (poll is not null)
            {
                var MappedPoll = _mapper.Map<Poll>(poll);
                var CreatedPoll = await _pollRepos.CreateAsync(MappedPoll, cancellationToken);
                if (CreatedPoll is not null)
                {
                    return _mapper.Map<PollsResponse>(CreatedPoll);
                }

            }
            return null;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var GetPoll = await GetByIdAsync(id, cancellationToken);
            if (GetPoll is not null)
            {
                var MappedPoll = _mapper.Map<Poll>(GetPoll);
                var Deleted = await _pollRepos.DeleteAsync(MappedPoll, cancellationToken);
                if (Deleted > 0)
                {
                    return true;
                }
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

        //public async Task<bool> ToggleIsPublishedAsync(int id, CancellationToken cancellationToken)
        //{
        //    var GetPoll = await GetByIdAsync(id, cancellationToken);
        //    if (GetPoll is not null)
        //    {
        //        var Mappedpoll = _mapper.Map<Poll>(GetPoll);
        //        Mappedpoll.IsPublished = !Mappedpoll.IsPublished;
        //        var Toggled = await _surveyBasketDbContext.SaveChangesAsync(cancellationToken);
        //        if (Toggled > 0)
        //            return true;
        //    }
        //    return false;
        //}

        public async Task<PollsResponse?> UpdateAsync(int id, CreatePollsRequest poll, CancellationToken cancellationToken)
        {
            var GetPoll = await GetByIdAsync(id, cancellationToken);
            if (GetPoll is not null)
            {
                var MappedPoll = _mapper.Map<Poll>(GetPoll);
                var Updated = await _pollRepos.UpdateAsync(MappedPoll, cancellationToken);
                if (Updated > 0)
                {
                    return _mapper.Map<PollsResponse>(MappedPoll);
                }
            }
            return null;
        }
    }
}
