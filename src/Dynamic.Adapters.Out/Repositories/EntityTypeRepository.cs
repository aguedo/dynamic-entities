using Dynamic.Application.Ports.Out.Repositories;
using Dynamic.Domain.Models;

namespace Dynamic.Adapters.Out.Repositories;

public class EntityTypeRepository : IEntityTypeRepository
{
    private readonly IGenericDbContext _dbContext;

    public EntityTypeRepository(IGenericDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EntityType> CreateAsync(EntityType entityType)
    {
        entityType.Id = Guid.NewGuid().ToString();
        return await _dbContext.AddAsync(entityType);
    }

    public async Task<IEnumerable<EntityType>> GetAllAsync()
    {
        return await _dbContext.GetAllAsync<EntityType>();
    }
}
