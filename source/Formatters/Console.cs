﻿using System;
using System.Linq;
using CommandLineEngine.Parser;

namespace CommandLineEngine.Formatters
{
    /// <summary>
    /// Formatter used to output to console
    /// </summary>
    public class Console : ConsoleBase
    {
        #region Class Construction

        /// <summary>
        /// Formatter used to output to console
        /// </summary>
        public Console()
        {
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
            WriteLine(String.Format(Resources.Help_Usage, executableName));

            // Write commands
            WriteLine();
            WriteLine(Resources.Help_AvailableCommands);
            configuration.Commands.OrderBy(i => i.Name).ForEach(c =>
                {
                    WriteLine(32, GenerateCommandString(configuration, c));
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
            WriteLine(String.Format(Resources.Help_ForCommand, command.Name));
            if (!String.IsNullOrEmpty(command.HelpUrl))
            {
                WriteLine(command.HelpUrl);
            }

            // Parameters
            if (parameters.Any())
            {
                WriteLine();
                WriteLine(Resources.Help_Parameters);
                parameters.ForEach(p =>
                    {
                        WriteLine(22, GenerateParameterString(configuration, p));
                    });
            }

            // Options
            if (options.Any())
            {
                WriteLine();
                WriteLine(Resources.Help_Options);
                options.ForEach(p =>
                    {
                        WriteLine(22, GenerateParameterString(configuration, p));
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
                    WriteLine(e.Message, System.ConsoleColor.Red);
                }
                WriteLine();
            }

            // Write program name
            if (!String.IsNullOrEmpty(configuration.Program.Name))
            {
                WriteLine($"{configuration.Program.Name} ({System.Reflection.Assembly.GetEntryAssembly().GetName().Version})");
                if (!String.IsNullOrEmpty(configuration.Program.Description))
                {
                    WriteLine(configuration.Program.Description);
                }
                if (!String.IsNullOrEmpty(configuration.Program.HelpUrl))
                {
                    WriteLine(configuration.Program.HelpUrl);
                }
                WriteLine();
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
    }
}
