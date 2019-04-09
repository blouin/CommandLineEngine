using System;
using System.Linq;
using System.Collections.Generic;

namespace CommandLineEngine.Parser
{
    /// <summary>
    /// Represents the configuration returned from command runner
    /// </summary>
    public sealed class Configuration
    {
        #region Class Construction

        /// <summary>
        /// Represents the configuration returned from command runner
        /// </summary>
        internal Configuration()
        {
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Validates the configuration after parsing
        /// </summary>
        /// <returns>Operation result</returns>
        internal Operation.OperationResult Validate()
        {
            var operationResult = new Operation.OperationResult();

            // Run user actions
            var b = ConfigurationValidationAction?.Invoke(operationResult);
            operationResult.Valid &= b ?? true;

            // Ensure there is at least one command
            if (!Commands.Any())
            {
                operationResult.Messages.Add(Resources.NoCommandFound);
            }

            // Ensure no command has a space in it
            var spacedParameters = Commands.Where(i => i.Name.Contains(" "));
            spacedParameters.ForEach(i => operationResult.Messages.Add(String.Format(Resources.CommandCanNotHaveSpace, i)));

            // Ensure no command has a reserved keyword
            if (Commands.Any(i => HelpCommandNames.Any(j => String.Compare(j, i.Name, true) == 0)))
            {
                operationResult.Messages.Add(String.Format(Resources.CommandReservedKeywords, String.Join("', '", HelpCommandNames)));
            }

            // Ensure all commands have distinct names
            var groups = Commands.Select(i => i.Name.ToLower()).ToLookup(i => i);
            if (groups.Any(i => i.Count() > 1))
            {
                var duplicate = groups.First(i => i.Count() > 1).Key;
                operationResult.Messages.Add(String.Format(Resources.CommandDuplicate, duplicate));
            }

            // Ensure each command is valid
            Commands.ForEach(i => i.ValidateConfigurationInternal(operationResult));

            return operationResult;
        }

        /// <summary>
        /// Gets the requested command
        /// </summary>
        /// <param name="args">Command arguments</param>
        /// <returns>Command requested from user</returns
        internal Command GetCommand(string[] args)
        {
            if (args.Length == 0)
            {
                return null;
            }

            // Command name is first args value (by convention)
            var arg = args[0];
            if (IsArgument(arg))
            {
                return null;
            }

            return Commands.FirstOrDefault(i => String.Compare(i.Name, arg, true) == 0);
        }

        /// <summary>
        /// Checks if the any of the arguments is the help switch
        /// </summary>
        /// <param name="args">Command arguments</param>
        /// <returns>True if help was requested</returns>
        internal bool GetArgumentHelp(string[] args)
        {
            return args.Any(j =>
                {
                    // Checks if the help command
                    if (HelpCommandNames.Any(i => String.Compare(j, i, true) == 0))
                    {
                        return true;
                    }

                    return false;
                });
        }

        /// <summary>
        /// Checks if the value passed is an argument
        /// </summary>
        /// <param name="value">Value passed</param>
        /// <returns>True if argument, otherwise false</returns>
        internal bool IsArgument(string value)
        {
            // Ensure the name is not a parameter
            return value.StartsWith(ShortParameterPrefix, StringComparison.OrdinalIgnoreCase) || value.StartsWith(LongParameterPrefix, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the program information
        /// </summary>
        public Program Program { get; private set; } = new Program();

        /// <summary>
        /// Gets or sets the prefix for parameters in their long name form
        /// </summary>
        public string LongParameterPrefix { get; set; } = "--";

        /// <summary>
        /// Gets or sets the prefix for parameters in their short name form
        /// </summary>
        public string ShortParameterPrefix { get; set; } = "-";

        /// <summary>
        /// Gets or sets the keywords reserved for help command
        /// </summary>
        public string[] HelpCommandNames { get; set; } = new[] { "--help", "-h", "?" };

        /// <summary>
        /// Gets the list of commands
        /// </summary>
        public IEnumerable<Command> Commands { get; internal set; }

        /// <summary>
        /// Gets or sets a user configurable action
        /// </summary>
        public Func<Operation.OperationResult, bool> ConfigurationValidationAction { get; set; }

        #endregion
    }
}
