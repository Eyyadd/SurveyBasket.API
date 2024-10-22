namespace SurveyBasket.API.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SurveyBasketDbContext _surveyBasketDbContext;
        protected DbSet<T> _set;

        public GenericRepository(SurveyBasketDbContext surveyBasketDbContext)
        {
            _surveyBasketDbContext = surveyBasketDbContext;
            _set = _surveyBasketDbContext.Set<T>();

        }

        public async Task<T?> CreateAsync(T Entity, CancellationToken cancellationToken)
        {
            var CreatedEntity = await _surveyBasketDbContext.AddAsync(Entity, cancellationToken);
            var CreatedAffected = await _surveyBasketDbContext.SaveChangesAsync(cancellationToken);
            if (CreatedAffected > 0)
                return CreatedEntity.Entity;
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
            var UpdatedEntity = _set.Update(Entity);
            var UpdatedAffected = await _surveyBasketDbContext.SaveChangesAsync(cancellationToken);
            return UpdatedAffected;
        }
    }
}
