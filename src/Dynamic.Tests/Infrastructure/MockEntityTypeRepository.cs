using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dynamic.Application.Ports.Out.Repositories;
using Dynamic.Domain.Models;

namespace Dynamic.Tests.Infrastructure
{
    public class MockEntityTypeRepository : IEntityTypeRepository
    {
        private readonly Dictionary<string, EntityType> _entityTypes = new Dictionary<string, EntityType>();

        public Task<EntityType> CreateAsync(EntityType entityType)
        {
            if (string.IsNullOrEmpty(entityType.Id))
            {
                entityType.Id = Guid.NewGuid().ToString();
            }

            _entityTypes[entityType.Id!] = entityType;
            return Task.FromResult(entityType);
        }

        public Task<IEnumerable<EntityType>> GetAllEntityTypesAsync()
        {
            return Task.FromResult<IEnumerable<EntityType>>(_entityTypes.Values);
        }
        public Task<EntityType?> GetEntityTypeByIdAsync(Guid id)
        {
            string idString = id.ToString();
            if (_entityTypes.TryGetValue(idString, out var entityType))
            {
                return Task.FromResult<EntityType?>(entityType);
            }

            return Task.FromResult<EntityType?>(null);
        }
        public Task<EntityType?> UpdateEntityTypeAsync(EntityType entityType)
        {
            if (entityType.Id != null && _entityTypes.ContainsKey(entityType.Id))
            {
                _entityTypes[entityType.Id] = entityType;
                return Task.FromResult<EntityType?>(entityType);
            }

            return Task.FromResult<EntityType?>(null);
        }

        public Task<bool> DeleteEntityTypeAsync(Guid id)
        {
            string idString = id.ToString();
            var result = _entityTypes.Remove(idString);
            return Task.FromResult(result);
        }
    }
}
