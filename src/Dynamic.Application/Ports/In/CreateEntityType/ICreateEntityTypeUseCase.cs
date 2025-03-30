using System;

namespace Dynamic.Application.Ports.In.CreateEntityType;

public interface ICreateEntityTypeUseCase
{
    Task<CreateEntityTypeOutput> CreateAsync(CreateEntityTypeInput createEntityType);
}