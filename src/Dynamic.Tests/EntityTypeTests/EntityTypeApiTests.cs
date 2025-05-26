using Dynamic.Adapters.In.EntityType;
using Dynamic.Domain.Models;
using Dynamic.Tests.Infrastructure;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Dynamic.Tests.EntityTypeTests
{
    public class EntityTypeApiTests : IClassFixture<DynamicWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public EntityTypeApiTests(DynamicWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Create_EntityType_WithFields_ShouldReturn_Success()
        {
            // Arrange
            // Create a request with the sample fields (Name, Age, Married)
            var createRequest = new CreateEntityTypeRequest
            {
                Name = "Person",

            };

            var content = new StringContent(
                JsonSerializer.Serialize(createRequest),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PostAsync("/api/entity-type/create", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var responseContent = await response.Content.ReadFromJsonAsync<CreateEntityTypeResponse>(_jsonOptions);
            responseContent.Should().NotBeNull();
            responseContent!.Id.Should().NotBeNullOrEmpty();
            responseContent.Name.Should().Be("Person");

        }

        [Fact]
        public async Task Create_EntityType_WithInvalidName_ShouldReturn_BadRequest()
        {
            // Arrange
            var createRequest = new CreateEntityTypeRequest
            {
                Name = null
            };

            var content = new StringContent(
                JsonSerializer.Serialize(createRequest),
                Encoding.UTF8,
                "application/json");

            // Act
            var response = await _client.PostAsync("/api/entity-type/create", content);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
