using Dynamic.Application.Ports.In.Shared;

namespace Dynamic.Application.Ports.In.CreateEntityType;

public interface ICreateEntityTypeUseCase
{
    Task<Output<CreateEntityTypeOutput>> CreateAsync(CreateEntityTypeInput createEntityType);
}