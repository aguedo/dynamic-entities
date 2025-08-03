using System.Threading.Tasks;
using Dynamic.Application.Ports.In.EntityTypeQuery;
using Dynamic.Adapters.Out.Repositories;
using Dynamic.Domain.Models;
using Xunit;

namespace Dynamic.Tests.Application;

public class EntityTypeQueryHandlerDeleteTests
{
    [Fact]
    public async Task DeleteAsync_DeletesEntity_WhenIdExists()
    {
        // Arrange
        var repo = new EntityTypeRepository();
        var handler = new EntityTypeQueryHandler(repo);
        var entity = new EntityType { Name = "ToDelete" };
        var created = await repo.CreateAsync(entity);

        // Act
        var deleted = await handler.DeleteAsync(created.Id!);
        var result = await handler.GetByIdAsync(created.Id!);

        // Assert
        Assert.True(deleted);
        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_ReturnsFalse_WhenIdDoesNotExist()
    {
        // Arrange
        var repo = new EntityTypeRepository();
        var handler = new EntityTypeQueryHandler(repo);

        // Act
        var deleted = await handler.DeleteAsync("nonexistent-id");

        // Assert
        Assert.False(deleted);
    }
}
