namespace SurveyBasket.API.IRepositories
{
    public interface IGenericRepository<T>
    {
        Task<IReadOnlyList<T?>> GetAsync(CancellationToken cancellationToken);
        Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<int> UpdateAsync(T Entity, CancellationToken cancellationToken);
        Task<int> DeleteAsync(T Entity, CancellationToken cancellationToken);
        Task<T?> CreateAsync(T Entity, CancellationToken cancellationToken);
    }
}
