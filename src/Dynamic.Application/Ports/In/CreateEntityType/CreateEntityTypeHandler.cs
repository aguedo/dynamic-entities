namespace Dynamic.Application.Ports.In.CreateEntityType;

public class CreateEntityTypeHandler : ICreateEntityTypeUseCase
{
    public async Task<CreateEntityTypeOutput> CreateAsync(CreateEntityTypeInput createEntityType)
    {
        await Task.Yield();

        // Here you would typically work with domain objects and repository to create the entity type.

        return new CreateEntityTypeOutput
        {
        };
    }
}
