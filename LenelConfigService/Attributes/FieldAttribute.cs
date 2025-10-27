using System;

namespace LenelConfigService.Attributes
{
    /// <summary>
    /// Marks a property as a selectable field in the Configuration model.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldAttribute : Attribute
    {
    }
}
