using SurveyBasket.API.Core;
using SurveyBasket.API.DTOs.Polls;
using System.Threading;

namespace SurveyBasket.API.Services
{
    public class PollService : IPollService
    {
        private readonly SurveyBasketDbContext _surveyBasketDbContext;
        private readonly IMapper _mapper;

        public PollService(SurveyBasketDbContext surveyBasketDbContext, IMapper mapper)
        {
            _surveyBasketDbContext = surveyBasketDbContext;
            _mapper = mapper;
        }

        public async Task<PollsResponse?> CreateAsync(CreatePollsRequest poll, CancellationToken cancellationToken)
        {
            if (poll is not null)
            {
                var MappedPoll = _mapper.Map<Poll>(poll);
                await _surveyBasketDbContext.AddAsync(MappedPoll);
                var Created = await _surveyBasketDbContext.SaveChangesAsync();
                if (Created > 0)
                {
                    return _mapper.Map<PollsResponse>(Created);
                }
            }
            return null;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var GetPoll = await GetByIdAsync(id, cancellationToken);
            if (GetPoll is not null)
            {
                var MappedPoll = _mapper.Map<Poll>(GetPoll);
                _surveyBasketDbContext.Remove(MappedPoll);
                var Deleted = await _surveyBasketDbContext.SaveChangesAsync();
                if (Deleted > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<IReadOnlyList<PollsResponse>> GetAsync(CancellationToken cancellationToken)
        {
            var Polls = await _surveyBasketDbContext.Polls.AsNoTracking().ToListAsync(cancellationToken);
            var MappedPolls = _mapper.Map<IReadOnlyList<PollsResponse>>(Polls);
            return MappedPolls;
        }

        public async Task<PollsResponse?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var GetPoll = await _surveyBasketDbContext.Polls
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id,cancellationToken);

            if (GetPoll is not null)
            {
                var MappedPolls = _mapper.Map<PollsResponse>(GetPoll);
                return MappedPolls;
            }
            return null;
        }

        public async Task<PollsResponse?> Update(int id, CreatePollsRequest poll, CancellationToken cancellationToken)
        {
            var GetPoll = await GetByIdAsync(id, cancellationToken);
            if (GetPoll is not null)
            {
                var MappedPoll = _mapper.Map<Poll>(GetPoll);
                _surveyBasketDbContext.Update(MappedPoll);
                var Updated = await _surveyBasketDbContext.SaveChangesAsync();
                if(Updated > 0)
                {
                    return _mapper.Map<PollsResponse>(MappedPoll);
                }
            }
            return null;
        }
    }
}
