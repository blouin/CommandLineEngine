using System;
using System.Linq;
using System.Text;
using CommandLineEngine.Parser;

namespace CommandLineEngine.Formatters
{
    /// <summary>
    /// Formatter used to output to a string builder
    /// </summary>
    public class Text : FormatterBase
    {
        #region Class Construction

        /// <summary>
        /// Formatter used to output to a string builder
        /// </summary>
        public Text()
            : this(null)
        {
        }

        /// <summary>
        /// Formatter used to output to a string builder
        /// </summary>
        /// <param name="stringBuilder">Current string builder, or null to create one</param>
        public Text(StringBuilder stringBuilder)
        {
            this.StringBuilder = stringBuilder ?? new StringBuilder();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Print help for the entire configuration (no command specified)
        /// </summary>
        /// <param name="configuration">Full configuration</param>
        protected virtual void PrintHelpConfiguration(Configuration configuration)
        {
            // Write program name
            var executableName = System.IO.Path.GetFileName(Environment.GetCommandLineArgs()[0]);
            StringBuilder.AppendLine(String.Format(Resources.Help_Usage, executableName));

            // Write commands
            StringBuilder.AppendLine();
            StringBuilder.AppendLine(Resources.Help_AvailableCommands);
            configuration.Commands.OrderBy(i => i.Name).ForEach(c =>
                {
                    StringBuilder.AppendLine(GenerateCommandString(configuration, c));
                });
        }

        /// <summary>
        /// Print help for a single command
        /// </summary>
        /// <param name="configuration">Full configuration</param>
        /// <param name="command">Specific command to output</param>
        protected virtual void PrintHelpCommand(Configuration configuration, Command command)
        {
            var parameters = command.Parameters.Where(i => i.Visible && !i.HasDefaultValue).OrderBy(i => i.Name);
            var options = command.Parameters.Where(i => i.Visible && i.HasDefaultValue).OrderBy(i => i.Name);

            // Display command
            StringBuilder.AppendLine(String.Format(Resources.Help_ForCommand, command.Name));
            if (!String.IsNullOrEmpty(command.HelpUrl))
            {
                StringBuilder.AppendLine(command.HelpUrl);
            }

            // Parameters
            if (parameters.Any())
            {
                StringBuilder.AppendLine();
                StringBuilder.AppendLine(Resources.Help_Parameters);
                parameters.ForEach(p =>
                    {
                        StringBuilder.AppendLine(GenerateParameterString(configuration, p));
                    });
            }

            // Options
            if (options.Any())
            {
                StringBuilder.AppendLine();
                StringBuilder.AppendLine(Resources.Help_Options);
                options.ForEach(p =>
                    {
                        StringBuilder.AppendLine(GenerateParameterString(configuration, p));
                    });
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prints help to formatter
        /// </summary>
        /// <param name="configuration">Full configuration</param>
        /// <param name="command">Specific command to output</param>
        /// <param name="operationMessages">List of operation message</param>
        public override sealed void PrintHelp(Configuration configuration, Command command, Operation.Items operationMessages)
        {
            // Write errors
            var errors = operationMessages.Where(i => i.Type == Operation.ItemType.Error);
            if (errors.Any())
            {
                foreach (var e in errors)
                {
                    StringBuilder.AppendLine(e.Message);
                }
                StringBuilder.AppendLine();
            }

            // Write program name
            if (!String.IsNullOrEmpty(configuration.Program.Name))
            {
                StringBuilder.AppendLine($"{configuration.Program.Name} ({System.Reflection.Assembly.GetEntryAssembly().GetName().Version})");
                if (!String.IsNullOrEmpty(configuration.Program.Description))
                {
                    StringBuilder.AppendLine(configuration.Program.Description);
                }
                if (!String.IsNullOrEmpty(configuration.Program.HelpUrl))
                {
                    StringBuilder.AppendLine(configuration.Program.HelpUrl);
                }
                StringBuilder.AppendLine();
            }

            // Single command, or multiple command
            if (command != null)
            {
                PrintHelpCommand(configuration, command);
            }
            else
            {
                PrintHelpConfiguration(configuration);
            }

        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Gets if we exit the program on error
        /// </summary>
        protected internal override sealed bool ExitOnError { get { return false; } }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the output string builder
        /// </summary>
        public StringBuilder StringBuilder { get; private set; }

        #endregion
    }
}
