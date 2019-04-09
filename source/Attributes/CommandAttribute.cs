using System;

namespace CommandLineEngine.Attributes
{
    /// <summary>
    /// Attribute used to identify the function as a runnable command
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CommandAttribute : Attribute
    {
        #region Class Construction

        /// <summary>
        /// Attribute used to identify the function as a runnable command
        /// </summary>
        /// <param name="name">Command name</param>
        /// <param name="description">Command description</param>
        /// <param name="helpUrl">Help URL</param>
        public CommandAttribute(string name = null, string description = null, string helpUrl = null)
        {
            this.Name = name;
            this.Description = description;
            this.HelpUrl = helpUrl;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the command name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the command description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the URL for more help
        /// </summary>
        public string HelpUrl { get; private set; }

        #endregion
    }
}
