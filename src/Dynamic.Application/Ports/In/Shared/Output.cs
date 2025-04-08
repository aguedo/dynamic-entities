using Dynamic.Domain.Models;

namespace Dynamic.Application.Ports.In.Shared;

public class Output<T>
{
    public T? Data { get; set; }
    public List<Error>? Errors { get; set; }
    public bool IsValid => (Errors == null || Errors.Count == 0) && Data != null;

    public Output(T data)
    {
        Data = data;
    }

    public Output(params IEnumerable<Error> errors)
    {
        Errors = errors.ToList();
    }
}
