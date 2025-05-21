namespace Dynamic.Domain.Models;

using System.ComponentModel.DataAnnotations;

public enum FieldType
{
    [Display(Name = "string")]
    String,
    [Display(Name = "integer")]
    Integer,
    [Display(Name = "boolean")]
    Boolean,
    [Display(Name = "datetime")]
    DateTime,
    [Display(Name = "decimal")]
    Decimal,
    [Display(Name = "guid")]
    Guid
}

public static class FieldTypeExtensions
{
    private static Dictionary<FieldType, string> _fieldTypeDisplayNamesCache = new();
    public static string GetDisplayName(this FieldType fieldType)
    {
        try
        {
            return _fieldTypeDisplayNamesCache[fieldType];
        }
        catch (KeyNotFoundException)
        {
            var displayAttribute = fieldType.GetType()
            .GetField(fieldType.ToString())
            ?.GetCustomAttributes(typeof(DisplayAttribute), false)
            .FirstOrDefault() as DisplayAttribute;

            var name = displayAttribute?.Name ?? fieldType.ToString();
            _fieldTypeDisplayNamesCache.Add(fieldType, name);
            return name;
        }

    }
}
