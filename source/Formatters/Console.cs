using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CommandLineEngine.Parser;

namespace CommandLineEngine.Formatters
{
    /// <summary>
    /// Formatter used to output to console
    /// </summary>
    public class Console : IHelpFormatter
    {
        #region Class Construction

        /// <summary>
        /// Formatter used to output to console
        /// </summary>
        public Console()
        {
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Formats the end of the text. We make sure it ends with a period, and has a space after
        /// </summary>
        /// <param name="text"></param>
        /// <param name="putSpace">Put space after</param>
        /// <returns>Formatted text</returns>
        private string EnsureEndOfText(string text, bool putSpace)
        {
            text = text.TrimEnd();
            return (text.EndsWith(".") ? text : text + ".") + (putSpace ? " " : String.Empty);
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Utility function to write text to output
        /// </summary>
        protected virtual void WriteLine()
        {
            WriteLine(1, String.Empty, null);
        }

        /// <summary>
        /// Utility function to write text to output
        /// </summary>
        /// <param name="text">Text to output</param>
        protected virtual void WriteLine(string text)
        {
            WriteLine(2, text, null);
        }

        /// <summary>
        /// Utility function to write text to output
        /// </summary>
        /// <param name="text">Text to output</param>
        /// <param name="color">Color text to output</param>
        protected virtual void WriteLine(string text, System.ConsoleColor? color)
        {
            WriteLine(2, text, color);
        }

        /// <summary>
        /// Utility function to write text to output
        /// </summary>
        /// <param name="lineOffset">Length of tab for new lines</param>
        /// <param name="text">Text to output</param>
        protected virtual void WriteLine(int lineOffset, string text)
        {
            WriteLine(lineOffset, text, null);
        }

        /// <summary>
        /// Utility function to write text to output
        /// </summary>
        /// <param name="lineOffset">Length of tab for new lines</param>
        /// <param name="text">Text to output</param>
        /// <param name="color">Color text to output</param>
        protected virtual void WriteLine(int lineOffset, string text, System.ConsoleColor? color)
        {
            int consoleWidth = System.Console.WindowWidth;
            int length = consoleWidth - lineOffset - 1;

            List<string> textLines = new List<string>();
            if (text.Length <= consoleWidth)
            {
                textLines.Add(text);
            }
            else
            {
                text = text.Replace(Environment.NewLine, String.Empty);

                // Remove first line
                textLines.Add(text.Substring(0, consoleWidth - 1));
                text = text.Remove(0, consoleWidth - 1);

                // Add rest in lines
                string regex = @"(?<=\G.{" + length + "})";
                var lines = Regex.Split(text, regex, RegexOptions.Singleline);
                foreach (var line in lines)
                {
                    var sb = new StringBuilder();
                    for (int i = 0; i < lineOffset; i++)
                    {
                        sb.Append(" ");
                    }
                    if (line[0] == ' ')
                    {
                        sb.Append(line.Remove(0, 1));
                    }
                    else
                    {
                        sb.Append(line);
                    }
                    textLines.Add(sb.ToString());
                }
            }

            // Add lines
            foreach (var textLine in textLines)
            {
                if (color.HasValue)
                {
                    System.Console.ForegroundColor = color.Value;
                }
                System.Console.WriteLine(textLine);
                System.Console.ResetColor();
            }
        }

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
                    WriteLine(32,
                        String.Format("  {0,-30}{1}{2}",
                        c.Name,
                        !String.IsNullOrEmpty(c.Description) ? EnsureEndOfText(c.Description, true) : Resources.Help_NoDescription,
                        !String.IsNullOrEmpty(c.HelpUrl) ? String.Format(Resources.Help_HelpUrl, c.HelpUrl) : String.Empty)
                    );
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
                        WriteLine(22,
                            String.Format("  {0,-20}{1}",
                            $"{configuration.LongParameterPrefix}{p.Name}{(!String.IsNullOrEmpty(p.ShortName) ? ", " + configuration.ShortParameterPrefix + p.ShortName : String.Empty)}",
                            !String.IsNullOrEmpty(p.Description) ? EnsureEndOfText(p.Description, false) : Resources.Help_NoDescription)
                        );
                    });
            }

            // Options
            if (options.Any())
            {
                WriteLine();
                WriteLine(Resources.Help_Options);
                options.ForEach(p =>
                    {
                        WriteLine(22,
                                String.Format("  {0,-20}{1}{2}",
                                $"{configuration.LongParameterPrefix}{p.Name}{(!String.IsNullOrEmpty(p.ShortName) ? ", " + configuration.ShortParameterPrefix + p.ShortName : String.Empty)}",
                                !String.IsNullOrEmpty(p.Description) ? EnsureEndOfText(p.Description, true) : Resources.Help_NoDescription,
                                String.Format(Resources.Help_DefaultValue, p.DefaultValue))
                            );
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
        public void PrintHelp(Configuration configuration, Command command, Operation.Items operationMessages)
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
