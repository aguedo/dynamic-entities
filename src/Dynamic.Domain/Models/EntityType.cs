namespace Dynamic.Domain.Models;

public class EntityType
{
    private readonly List<IField> _fields = [];

    public string? Id { get; set; }
    public string? Name { get; set; }
    public IReadOnlyList<IField> Fields => _fields;

    public void AddFields(params IEnumerable<IField> fields)
    {
        ArgumentNullException.ThrowIfNull(fields);
        _fields.AddRange(fields);
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
