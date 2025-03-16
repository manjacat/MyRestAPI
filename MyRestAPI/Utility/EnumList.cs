using System.ComponentModel;
using System.Reflection;

namespace MyRestAPI.Utility
{
    public static class EnumList
    {
        public static string GetDescription(this Enum value)
        {
            string? description = value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
            if (string.IsNullOrEmpty(description)) 
            {
                description = string.Empty;
            }
            return description;
        }

        public enum ErrorMessage
        {
            [Description("Access Denied")]
            AccessDenied = 0,
            [Description("Invalid Total Amount")]
            InvalidTotalAmount = 1,
            [Description("Expired")]
            Expired = 2,
            [Description("Model Validation Failed")]
            MissingParameter = 3,
            [Description("Internal Server Error")]
            InternalServerError = 4,
        }
    }
}
