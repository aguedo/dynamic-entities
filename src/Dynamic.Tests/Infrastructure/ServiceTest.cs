using Moq;
using System.Text.Json;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;
using System.Threading.Tasks;
using FluentAssertions;
using Dynamic.Adapters.In.EntityType;
using Dynamic.Application.Ports.In.CreateEntityType;
using Dynamic.Application.Ports.In.Shared;
using Dynamic.Application.Ports.In.EntityTypeQuery;
using Dynamic.Application.Ports.In.UpdateEntityType;
using Dynamic.Application.Ports.In.DeleteEntityType;

namespace Dynamic.Tests
{
    /// <summary>
    /// This consolidated test class covers both service-level and API-level tests for EntityType.
    /// ApiTests - Tests for the EntityType API endpoints
    /// </summary>
    public class ServiceTest
    {
        [Fact]
        public async Task CreateEntityType_ApiIntegration_ShouldReturn_Success()
        {
            // Arrange: set up test server
            using var factory = new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<Program>();
            var client = factory.CreateClient();

            var createRequest = new CreateEntityTypeRequest { Name = "Person" };
            var content = new StringContent(
                JsonSerializer.Serialize(createRequest),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await client.PostAsync("/api/entity-type/create", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseContent = await response.Content.ReadFromJsonAsync<CreateEntityTypeResponse>(new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            responseContent.Should().NotBeNull();
            responseContent!.Id.Should().NotBeNullOrEmpty();
            responseContent.Name.Should().Be("Person");
        }

        [Fact]
        public async Task CreateEntityType_ApiMock_ShouldReturn_Success()
        {
            // Arrange: mock the use case
            var mockUseCase = new Mock<ICreateEntityTypeUseCase>();
            var request = new CreateEntityTypeRequest { Name = "Person" };
            var input = new CreateEntityTypeInput { Name = "Person" };
            var expectedOutput = new Output<CreateEntityTypeOutput>
            {
                Data = new CreateEntityTypeOutput { Id = "123", Name = "Person" }
            };
            mockUseCase.Setup(u => u.CreateAsync(It.Is<CreateEntityTypeInput>(i => i.Name == "Person")))
                .ReturnsAsync(expectedOutput);

            // Simulate controller call
            var mockQueryUseCase = new Mock<IEntityTypeQueryUseCase>();
            var mockUpdateUseCase = new Mock<IUpdateEntityTypeUseCase>();
            var mockDeleteUseCase = new Mock<IDeleteEntityTypeUseCase>();

            var controller = new EntityTypeController(
                mockUseCase.Object,
                mockQueryUseCase.Object,
                mockUpdateUseCase.Object,
                mockDeleteUseCase.Object);
            var actionResult = await controller.Create(request);

            // Extract the response from IActionResult
            var okResult = actionResult as Microsoft.AspNetCore.Mvc.OkObjectResult;
            okResult.Should().NotBeNull();
            var result = okResult.Value as CreateEntityTypeResponse;

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be("123");
            result.Name.Should().Be("Person");
        }
    }
}
