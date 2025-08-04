using Dynamic.Application.Ports.Out.Repositories;
using Dynamic.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dynamic.Adapters.Out.Repositories;


public class EntityTypeEfRepository : IEntityTypeRepository
{
    private readonly DbContext _dbContext;

    public EntityTypeEfRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<EntityType> CreateAsync(EntityType entityType)
    {
        entityType.Id = Guid.NewGuid().ToString();
        _dbContext.Set<EntityType>().Add(entityType);
        await _dbContext.SaveChangesAsync();
        return entityType;
    }


    public async Task<IEnumerable<EntityType>> GetAllAsync()
    {
        return await _dbContext.Set<EntityType>().ToListAsync();
    }


    public async Task<EntityType?> GetByIdAsync(string id)
    {
        return await _dbContext.Set<EntityType>().FirstOrDefaultAsync(e => e.Id == id);
    }


    public async Task<EntityType?> UpdateAsync(EntityType entityType)
    {
        var existing = await _dbContext.Set<EntityType>().FirstOrDefaultAsync(e => e.Id == entityType.Id);
        if (existing == null)
            return null;
        existing.Name = entityType.Name;
        // Add field updates here if needed
        await _dbContext.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var entity = await _dbContext.Set<EntityType>().FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null)
            return false;
        _dbContext.Set<EntityType>().Remove(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
