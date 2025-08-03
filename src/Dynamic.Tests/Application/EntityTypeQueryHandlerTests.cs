using System.Threading.Tasks;
using Dynamic.Application.Ports.In.EntityTypeQuery;
using Dynamic.Adapters.Out.Repositories;
using Dynamic.Domain.Models;
using Xunit;

namespace Dynamic.Tests.Application;

public class EntityTypeQueryHandlerTests
{
    [Fact]
    public async Task GetByIdAsync_ReturnsEntity_WhenIdExists()
    {
        // Arrange
        var repo = new EntityTypeRepository();
        var handler = new EntityTypeQueryHandler(repo);
        var entity = new EntityType { Name = "TestType" };
        var created = await repo.CreateAsync(entity);

        // Act
        var result = await handler.GetByIdAsync(created.Id!);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(created.Id, result!.Id);
        Assert.Equal("TestType", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenIdDoesNotExist()
    {
        // Arrange
        var repo = new EntityTypeRepository();
        var handler = new EntityTypeQueryHandler(repo);

        // Act
        var result = await handler.GetByIdAsync("nonexistent-id");

        // Assert
        Assert.Null(result);
    }
}
