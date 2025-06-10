using Dynamic.Domain.Models;
using Xunit;

namespace Dynamic.Tests.Domain;

public class EntityTypeTests
{
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
}
