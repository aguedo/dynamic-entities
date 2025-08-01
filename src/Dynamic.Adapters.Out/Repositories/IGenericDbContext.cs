using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dynamic.Adapters.Out.Repositories
{
    public interface IGenericDbContext
    {
        Task<TEntity> AddAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : class;
    }
}
