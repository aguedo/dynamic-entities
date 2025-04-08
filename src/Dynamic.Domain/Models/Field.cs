namespace Dynamic.Domain.Models;

public class Field
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public FieldType? Type { get; set; }
    public EntityType? EntityType { get; set; }
}
