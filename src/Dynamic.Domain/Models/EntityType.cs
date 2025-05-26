using System.Text.Json.Nodes;

namespace Dynamic.Domain.Models;

public class EntityType
{
    private readonly List<IField> _fields = [];

    public string? Id { get; set; }
    public string? Name { get; set; }
    public IReadOnlyList<IField> Fields => _fields;

    public void AddFields(params IField[] fields)
    {
        ArgumentNullException.ThrowIfNull(fields);
        foreach (var field in fields)
        {
            var newName = field.Header?.Name;
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException($"Field name cannot be null or whitespace. Fields: {System.Text.Json.JsonSerializer.Serialize(fields)}");
            }
            if (!_fields.Any(existing => existing.Header?.Name == newName))
            {
                _fields.Add(field);
            }
        }
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
