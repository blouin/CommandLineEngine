using System;
using System.Collections.Generic;
using System.Text;

namespace CommandLineEngine.Parser
{
    /// <summary>
    /// Information regarding the program
    /// </summary>
    public sealed class Program
    {
        #region Class Construction

        /// <summary>
        /// Information regarding the program
        /// </summary>
        internal Program()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a string representation of the program
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the command name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the command description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the URL for more help
        /// </summary>
        public string HelpUrl { get; set; }

#endregion
    }
}
