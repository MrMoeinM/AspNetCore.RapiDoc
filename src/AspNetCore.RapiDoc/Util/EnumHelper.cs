using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AspNetCore.RapiDoc.Util
{
    public static class EnumHelper //<T> where T : struct, Enum
    {
        private static string lookupResource(Type resourceManagerProvider, string resourceKey)
        {
            var resourceKeyProperty = resourceManagerProvider.GetProperty(resourceKey,
                BindingFlags.Static | BindingFlags.Public, null, typeof(string),
                new Type[0], null);
            if (resourceKeyProperty != null)
            {
                return (string)resourceKeyProperty.GetMethod.Invoke(null, null);
            }

            return resourceKey; // Fallback with the key name
        }

        public static string GetDisplayValue<T>(this T value) where T : struct , Enum
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
                return string.Empty;

            var attr = fieldInfo.GetCustomAttribute<DisplayAttribute>();

            if (attr == null)
                return string.Empty;

            if(attr.ResourceType != null)
                return lookupResource(attr.ResourceType, attr.Name);

            return attr.Name;
        }

    }
}
