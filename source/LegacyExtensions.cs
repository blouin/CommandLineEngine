using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CommandLineEngine
{

#if NET35
    static class LegacyExtensions
    {
        internal static T GetCustomAttribute<T>(this MethodInfo i)
            where T : Attribute
        {
            return (T)i.GetCustomAttributes(typeof(T), true).SingleOrDefault();
        }
        internal static T GetCustomAttribute<T>(this ParameterInfo i)
            where T : Attribute
        {
            return (T)i.GetCustomAttributes(typeof(T), true).SingleOrDefault();
        }
        internal static IEnumerable<T> GetCustomAttributes<T>(this ParameterInfo i)
            where T : Attribute
        {
            return i.GetCustomAttributes(typeof(T), true).Cast<T>();
        }
        internal static bool HasDefaultValue(this ParameterInfo i)
        {
            return ((i.Attributes & ParameterAttributes.HasDefault) == ParameterAttributes.HasDefault);
        }
    }
#endif
}
