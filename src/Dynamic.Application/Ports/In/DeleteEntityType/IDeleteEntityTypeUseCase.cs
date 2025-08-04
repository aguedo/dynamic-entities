using Dynamic.Domain.Models;

namespace Dynamic.Application.Ports.In.DeleteEntityType;

public interface IDeleteEntityTypeUseCase
{
    Task<bool> DeleteAsync(string id);
}
