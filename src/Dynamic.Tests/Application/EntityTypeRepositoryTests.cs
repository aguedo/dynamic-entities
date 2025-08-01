using System.Threading.Tasks;
using Dynamic.Adapters.Out.Repositories;
using Dynamic.Domain.Models;
using Xunit;
using System.Linq;

namespace Dynamic.Tests.Application;

public class EntityTypeRepositoryTests
{
    [Fact]
    public async Task CreateAndGetAll_WorksCorrectly()
    {
        var repo = new EntityTypeRepository();

        var entity = new EntityType { Name = "TestType" };
        var created = await repo.CreateAsync(entity);

        Assert.NotNull(created.Id);
        Assert.Equal("TestType", created.Name);

        var all = await repo.GetAllAsync();
        Assert.Single(all);
        Assert.Equal("TestType", all.First().Name);
    }
}
