using System.Threading.Tasks;
using Dynamic.Application.Ports.In.EntityTypeQuery;
using Dynamic.Adapters.Out.Repositories;
using Dynamic.Domain.Models;
using Xunit;

namespace Dynamic.Tests.Application;

public class EntityTypeQueryHandlerUpdateTests
{
    [Fact]
    public async Task UpdateAsync_UpdatesEntity_WhenIdExists()
    {
        // Arrange
        var repo = new EntityTypeRepository();
        var handler = new EntityTypeQueryHandler(repo);
        var entity = new EntityType { Name = "OriginalName" };
        var created = await repo.CreateAsync(entity);

        // Act
        var updated = await handler.UpdateAsync(created.Id!, "UpdatedName");

        // Assert
        Assert.NotNull(updated);
        Assert.Equal(created.Id, updated!.Id);
        Assert.Equal("UpdatedName", updated.Name);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsNull_WhenIdDoesNotExist()
    {
        // Arrange
        var repo = new EntityTypeRepository();
        var handler = new EntityTypeQueryHandler(repo);

        // Act
        var updated = await handler.UpdateAsync("nonexistent-id", "Name");

        // Assert
        Assert.Null(updated);
    }
}
