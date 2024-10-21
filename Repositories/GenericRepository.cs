using SurveyBasket.API.Core;
using SurveyBasket.API.IRepositories;

namespace SurveyBasket.API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SurveyBasketDbContext _surveyBasketDbContext;
        private DbSet<T> _set;

        public GenericRepository(SurveyBasketDbContext surveyBasketDbContext)
        {
            _surveyBasketDbContext = surveyBasketDbContext;
            _set = _surveyBasketDbContext.Set<T>();

        }

        public async Task<T?> CreateAsync(T Entity, CancellationToken cancellationToken)
        {
            await _surveyBasketDbContext.AddAsync(Entity, cancellationToken);
            var Created = await _surveyBasketDbContext.SaveChangesAsync(cancellationToken);
            if (Created > 0)
                return Entity;
            return default;
        }

        public async Task<int> DeleteAsync(T Entity, CancellationToken cancellationToken)
        {
            _surveyBasketDbContext.Remove(Entity);
            var Deleted = await _surveyBasketDbContext.SaveChangesAsync(cancellationToken);
            return Deleted;
        }

        public async Task<IReadOnlyList<T?>> GetAsync(CancellationToken cancellationToken)
        {
            var GetEntities = await _set.AsNoTracking().ToListAsync(cancellationToken);
            return GetEntities;
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var GetEntity = await _set.FindAsync(id, cancellationToken);
            return GetEntity;
        }

        public async Task<int> UpdateAsync(T Entity, CancellationToken cancellationToken)
        {
            _surveyBasketDbContext.Update(Entity);
            var Updated = await _surveyBasketDbContext.SaveChangesAsync(cancellationToken);
            return Updated;
        }
    }
}
