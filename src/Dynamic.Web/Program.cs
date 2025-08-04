
using Dynamic.Application.Ports.In.UpdateEntityType;
using Dynamic.Application.Ports.In.DeleteEntityType;
using Dynamic.Application.Ports.In.EntityTypeQuery;
using Dynamic.Adapters.In.EntityType;
using Dynamic.Adapters.Out.Repositories;

using Dynamic.Application.Ports.In.CreateEntityType;
using Dynamic.Application.Ports.Out.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(EntityTypeController).Assembly);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddScoped<IDeleteEntityTypeUseCase, DeleteEntityTypeHandler>();
builder.Services.AddScoped<ICreateEntityTypeUseCase, CreateEntityTypeHandler>();
builder.Services.AddScoped<IEntityTypeRepository, EntityTypeRepository>();
builder.Services.AddScoped<IEntityTypeQueryUseCase, EntityTypeQueryHandler>();
builder.Services.AddScoped<IUpdateEntityTypeUseCase, UpdateEntityTypeHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
