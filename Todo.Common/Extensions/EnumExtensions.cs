using System.ComponentModel;
using System.Reflection;

namespace Todo.Common.Extensions {
    public static class EnumExtensions {
        public static string GetDescription(this Enum value) {
            if (!Enum.IsDefined(value.GetType(), (int)(object)value)) {
                return "-";
            }

            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null && attributes.Length > 0) {
                return attributes[0].Description;
            }

            return value.ToString();
        }
    }
}