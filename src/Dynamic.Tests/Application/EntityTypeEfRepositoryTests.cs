using System.Threading.Tasks;
using System.Collections.Generic;
using Dynamic.Adapters.Out.Repositories;
using Dynamic.Domain.Models;
using Dynamic.Application.Ports.Out.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;


public class EntityTypeEfRepositoryTests
{
    // Minimal test DbContext with DbSet<EntityType>
    private class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
        public DbSet<EntityType> EntityTypes { get; set; } = null!;
    }


    private DbContextOptions<TestDbContext> CreateInMemoryOptions(string dbName)
    {
        return new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;
    }

    private TestDbContext CreateContext(string dbName)
    {
        var options = CreateInMemoryOptions(dbName);
        var context = new TestDbContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    private IEntityTypeRepository CreateRepository(TestDbContext context)
    {
        return new EntityTypeEfRepository(context);
    }


    [Fact]
    public async Task CreateAsync_AddsEntity()
    {
        var dbName = nameof(CreateAsync_AddsEntity) + System.Guid.NewGuid();
        using var context = CreateContext(dbName);
        var repo = CreateRepository(context);
        var entity = new EntityType { Name = "Test Entity" };
        var result = await repo.CreateAsync(entity);
        Assert.NotNull(result.Id);
        Assert.Equal("Test Entity", result.Name);
    }


    [Fact]
    public async Task GetAllAsync_ReturnsAllEntities()
    {
        var dbName = nameof(GetAllAsync_ReturnsAllEntities) + System.Guid.NewGuid();
        using var context = CreateContext(dbName);
        var repo = CreateRepository(context);
        await repo.CreateAsync(new EntityType { Name = "A" });
        await repo.CreateAsync(new EntityType { Name = "B" });
        var all = await repo.GetAllAsync();
        Assert.Equal(2, ((List<EntityType>)all).Count);
    }


    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectEntity()
    {
        var dbName = nameof(GetByIdAsync_ReturnsCorrectEntity) + System.Guid.NewGuid();
        using var context = CreateContext(dbName);
        var repo = CreateRepository(context);
        var entity = await repo.CreateAsync(new EntityType { Name = "FindMe" });
        var found = await repo.GetByIdAsync(entity.Id);
        Assert.NotNull(found);
        Assert.Equal("FindMe", found.Name);
    }


    [Fact]
    public async Task UpdateAsync_UpdatesEntity()
    {
        var dbName = nameof(UpdateAsync_UpdatesEntity) + System.Guid.NewGuid();
        using var context = CreateContext(dbName);
        var repo = CreateRepository(context);
        var entity = await repo.CreateAsync(new EntityType { Name = "OldName" });
        entity.Name = "NewName";
        var updated = await repo.UpdateAsync(entity);
        Assert.NotNull(updated);
        Assert.Equal("NewName", updated.Name);
    }


    [Fact]
    public async Task DeleteAsync_RemovesEntity()
    {
        var dbName = nameof(DeleteAsync_RemovesEntity) + System.Guid.NewGuid();
        using var context = CreateContext(dbName);
        var repo = CreateRepository(context);
        var entity = await repo.CreateAsync(new EntityType { Name = "ToDelete" });
        var deleted = await repo.DeleteAsync(entity.Id);
        Assert.True(deleted);
        var found = await repo.GetByIdAsync(entity.Id);
        Assert.Null(found);
    }
}
