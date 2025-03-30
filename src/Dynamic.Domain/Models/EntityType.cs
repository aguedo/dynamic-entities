namespace Dynamic.Domain.Models;

public class EntityType
{
    private readonly List<Field> _fields = [];

    public string? Id { get; set; }
    public string? Name { get; set; }
    public IReadOnlyList<Field> Fields => _fields;

    public void AddField(Field field)
    {
        ArgumentNullException.ThrowIfNull(field);
        _fields.Add(field);
    }

    public bool IsValid(out List<Error> errors)
    {
        errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(Name))
        {
            errors.Add(new Error { Message = "Name is required." });
        }

        return errors.Count == 0;
    }
}
