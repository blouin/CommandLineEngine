using System;

namespace CommandLineEngine.Attributes
{
    /// <summary>
    /// Attribute used to hide the parameter from help
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class ParameterHiddenAttribute : Attribute
    {
        #region Class Construction

        /// <summary>
        /// Attribute used to hide the parameter from help
        /// </summary>
        public ParameterHiddenAttribute()
        {
        }

        #endregion
    }
}
