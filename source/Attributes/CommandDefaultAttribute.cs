using System;

namespace CommandLineEngine.Attributes
{
    /// <summary>
    /// Attribute used to mark the command as the default
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CommandDefaultAttribute : Attribute
    {
        #region Class Construction

        /// <summary>
        /// Attribute used to mark the command as the default
        /// </summary>
        public CommandDefaultAttribute()
        {
        }

        #endregion
    }
}
