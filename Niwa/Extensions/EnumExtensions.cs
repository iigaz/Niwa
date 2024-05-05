using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Niwa.Extensions;

public static class EnumExtensions
{
    public static string? GetName(this Enum enumValue)
    {
        return enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?.Name;
    }
}