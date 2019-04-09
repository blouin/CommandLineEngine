using System;

namespace CommandLineEngine.Attributes
{
    /// <summary>
    /// Attribute used to set properties for the parameter
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public class ParameterAttribute : Attribute
    {
        #region Class Construction

        /// <summary>
        /// Attribute used to set properties for the parameter
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <param name="shortName">Parameter short name</param>
        /// <param name="description">Parameter description</param>
        public ParameterAttribute(string name = null, string shortName = null, string description = null)
        {
            this.Name = name;
            this.ShortName = shortName;
            this.Description = description;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the parameter name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the parameter short name
        /// </summary>
        public string ShortName { get; private set; }

        /// <summary>
        /// Gets the parameter description
        /// </summary>
        public string Description { get; private set; }

        #endregion
    }
}
