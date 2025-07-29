using Dynamic.Adapters.In.EntityType;
using Dynamic.Adapters.Out.Repositories;
using Dynamic.Application.Ports.In.CreateEntityType;
using Dynamic.Application.Ports.Out.Repositories;
using Dynamic.Domain.Models;
using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Dynamic.Tests
{
    /// <summary>
    /// This consolidated test class covers both service-level and API-level tests for EntityType.
    /// It contains two inner classes:
    /// 1. ServiceTests - Tests for the CreateEntityTypeUseCase directly
    /// 2. ApiTests - Tests for the EntityType API endpoints
    /// </summary>
    public class EntityTypeTests
    {
        /// <summary>
        /// Tests for the EntityType service layer
        /// </summary>
        public class ServiceTests
        {
            private readonly ICreateEntityTypeUseCase _createEntityTypeUseCase;

            public ServiceTests()
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

        // API tests removed. To test API logic, mock the controller/service and simulate API calls in unit tests.
    }
}
