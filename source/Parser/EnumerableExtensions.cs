using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineEngine.Parser
{
    /// <summary>
    /// Extensions for the enumerable
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Extension that adds an action for each item in the enumerble
        /// </summary>
        /// <typeparam name="T">Item in enumerable</typeparam>
        /// <param name="enumerable">Enumerable to iterate over</param>
        /// <param name="action">Action to execute for each item</param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }
    }
}
