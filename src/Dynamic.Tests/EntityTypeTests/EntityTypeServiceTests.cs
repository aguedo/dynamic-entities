using Dynamic.Adapters.In.EntityType;
using Dynamic.Adapters.Out.Repositories;
using Dynamic.Application.Ports.In.CreateEntityType;
using Dynamic.Application.Ports.Out.Repositories;
using Dynamic.Domain.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Dynamic.Tests.EntityTypeTests
{
    public class EntityTypeServiceTests
    {
        private readonly ICreateEntityTypeUseCase _createEntityTypeUseCase;

        public EntityTypeServiceTests()
        {
            // Set up the dependency injection container for our tests
            var services = new ServiceCollection();
            services.AddScoped<ICreateEntityTypeUseCase, CreateEntityTypeHandler>();
            services.AddScoped<IEntityTypeRepository, EntityTypeRepository>();

            var serviceProvider = services.BuildServiceProvider();
            _createEntityTypeUseCase = serviceProvider.GetRequiredService<ICreateEntityTypeUseCase>();
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
}
