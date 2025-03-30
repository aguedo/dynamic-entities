namespace Dynamic.Domain.Models;

public class Error
{
    public required string Message { get; set; }
    public string? Code { get; set; }
}
