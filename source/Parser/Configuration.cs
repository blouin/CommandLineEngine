﻿using System;
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
        /// <param name="countDefaultCommands">Count of default commands found</param>
        /// <returns>Operation result</returns>
        internal Operation.OperationResult Validate(int countDefaultCommands)
        {
            var operationResult = new Operation.OperationResult();

            // Run user actions
            var b = ConfigurationValidationAction?.Invoke(this, operationResult);
            if (b.HasValue && !b.Value)
            {
                operationResult.Messages.Add(Resources.ConfigurationValidationAction);
            }

            // Ensure there is at least one command
            if (!Commands.Any())
            {
                operationResult.Messages.Add(Resources.NoCommandFound);
            }

            // Ensure there is at most one default command
            if (countDefaultCommands > 1)
            {
                operationResult.Messages.Add(Resources.CommandDefaultMostOne);
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
            if (args.Length > 0 && !IsArgument(args[0]))
            {
                return Commands.FirstOrDefault(i => String.Compare(i.Name, args[0], true) == 0);
            }

            return DefaultCommand;
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
        /// Gets the default command
        /// </summary>
        public Command DefaultCommand { get; internal set; }

        /// <summary>
        /// Gets or sets a user configurable action
        /// </summary>
        public Func<Configuration, Operation.OperationResult, bool> ConfigurationValidationAction { get; set; }

        /// <summary>
        /// Gets or sets a user parse parameter action
        /// </summary>
        public Func<Configuration, Parser.Command, string[], InputArguments.AddArgumentDelegate, bool> ParseArgumentsAction { get; set; }

        #endregion
    }
}
