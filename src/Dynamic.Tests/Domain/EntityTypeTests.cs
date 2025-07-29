using Dynamic.Adapters.Out.Repositories;
using Dynamic.Application.Ports.In.CreateEntityType;
using Dynamic.Application.Ports.Out.Repositories;
using Dynamic.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

using System.Threading.Tasks;
using FluentAssertions;

namespace Dynamic.Tests.Domain;

public class EntityTypeTests
{

    private readonly ICreateEntityTypeUseCase _createEntityTypeUseCase;

    public EntityTypeTests()
    {
        // Set up the dependency injection container for our tests
        var services = new ServiceCollection();
        services.AddScoped<ICreateEntityTypeUseCase, CreateEntityTypeHandler>();
        services.AddScoped<IEntityTypeRepository, EntityTypeRepository>();

        var serviceProvider = services.BuildServiceProvider();
        _createEntityTypeUseCase = serviceProvider.GetRequiredService<ICreateEntityTypeUseCase>();
    }

    [Fact]
    public void IsValid_ReturnsTrue_WhenNameIsProvided()
    {
        var entity = new EntityType { Name = "Sample" };

        var result = entity.IsValid(out var errors);

        Assert.True(result);
        Assert.Empty(errors);
    }

    [Fact]
    public void IsValid_ReturnsFalse_WhenNameIsMissing()
    {
        var entity = new EntityType();

        var result = entity.IsValid(out var errors);

        Assert.False(result);
        Assert.Single(errors);
        Assert.Equal("Name is required.", errors[0].Message);
    }

    [Fact]
    public async Task Create_EntityType_WithFields_ShouldReturn_Success()
    {
        // Arrange
        var createEntityTypeInput = new CreateEntityTypeInput
        {
            Name = "Person",
        };

        // Act
        var result = await _createEntityTypeUseCase.CreateAsync(createEntityTypeInput);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data!.Id.Should().NotBeNullOrEmpty();
        result.Data.Name.Should().Be("Person");
    }

    [Fact]
    public async Task Create_EntityType_WithInvalidName_ShouldReturn_Error()
    {
        // Arrange
        var createEntityTypeInput = new CreateEntityTypeInput
        {
            Name = null
        };

        // Act
        var result = await _createEntityTypeUseCase.CreateAsync(createEntityTypeInput);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
        result.Errors.Should().NotBeNull();
        result.Errors!.Should().Contain(e => e.Message == "Name is required.");
    }
}
