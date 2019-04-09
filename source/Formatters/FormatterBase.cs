using System;
using CommandLineEngine.Parser;

namespace CommandLineEngine.Formatters
{
    /// <summary>
    /// Base formatter object
    /// </summary>
    public abstract class FormatterBase : IHelpFormatter
    {
        #region Class Construction
        
        /// <summary>
        /// Creates the base formatter
        /// </summary>
        protected FormatterBase()
        {
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Formats the end of the text. We make sure it ends with a period, and has a space after
        /// </summary>
        /// <param name="text">Text to format</param>
        /// <param name="putSpace">Put space after</param>
        /// <returns>Formatted text</returns>
        protected string EnsureEndOfText(string text, bool putSpace)
        {
            text = text.TrimEnd();
            return (text.EndsWith(".") ? text : text + ".") + (putSpace ? " " : String.Empty);
        }

        /// <summary>
        /// Generates the string to represent the command
        /// </summary>
        /// <param name="configuration">Current configuration</param>
        /// <param name="command">Command whose string to generate</param>
        /// <returns>Command help string</returns>
        protected virtual string GenerateCommandString(Configuration configuration, Command command)
        {
            return String.Format("  {0,-30}{1}{2}",
                command.Name,
                !String.IsNullOrEmpty(command.Description) ? EnsureEndOfText(command.Description, true) : Resources.Help_NoDescription,
                !String.IsNullOrEmpty(command.HelpUrl) ? String.Format(Resources.Help_HelpUrl, command.HelpUrl) : String.Empty);
        }

        /// <summary>
        /// Generates the string to represent the parameter
        /// </summary>
        /// <param name="configuration">Current configuration</param>
        /// <param name="parameter">Command whose string to generate</param>
        /// <returns>Parameter help string</returns>
        protected virtual string GenerateParameterString(Configuration configuration, CommandParameter parameter)
        {
            return String.Format("  {0,-20}{1}{2}",
                $"{configuration.LongParameterPrefix}{parameter.Name}{(!String.IsNullOrEmpty(parameter.ShortName) ? ", " + configuration.ShortParameterPrefix + parameter.ShortName : String.Empty)}",
                !String.IsNullOrEmpty(parameter.Description) ? EnsureEndOfText(parameter.Description, true) : Resources.Help_NoDescription,
                parameter.HasDefaultValue ? String.Format(Resources.Help_DefaultValue, parameter.DefaultValue) : String.Empty);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prints help to formatter
        /// </summary>
        /// <param name="configuration">Full configuration</param>
        /// <param name="command">Specific command to output</param>
        /// <param name="operationMessages">List of operation message</param>
        public abstract void PrintHelp(Configuration configuration, Command command, Operation.Items operationMessages);

        #endregion
    }
}
