namespace Dynamic.Domain.Models;

public class FieldHeader
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? DisplayName { get; set; }
    public FieldType? Type { get; set; }
    public EntityType? EntityType { get; set; }
}
