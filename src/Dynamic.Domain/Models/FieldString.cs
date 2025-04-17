namespace Dynamic.Domain.Models;

public class FieldString : IField
{
    public FieldHeader? Header { get; set; }
    public int? MaxLength { get; set; }
}
