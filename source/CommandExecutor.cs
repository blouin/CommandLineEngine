using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using CommandLineEngine.Attributes;

namespace CommandLineEngine
{
    /// <summary>
    /// Class used to run commands
    /// </summary>
    public static class CommandExecutor
    {
        /// <summary>
        /// Executes the command passed in the arguments
        /// </summary>
        /// <param name="args">List of retrieved arguments, or null to extract the arguments from the Environment</param>
        /// <param name="helpFormatter">Help formatting engine, or nothing to format the messages to console</param>
        /// <param name="configurationBuilder">Configuration builder action, or nothing to use defaults</param>
        /// <returns>Results from the operation</returns>
        public static Operation.OperationExecutionResult Execute(string[] args, IHelpFormatter helpFormatter = null, Action<Parser.Configuration> configurationBuilder = null)
        {
            return Execute(args, new Type[] { }, helpFormatter, configurationBuilder);
        }

        /// <summary>
        /// Executes the command passed in the arguments
        /// </summary>
        /// <param name="args">List of retrieved arguments, or null to extract the arguments from the Environment</param>
        /// <param name="type">Type in which to get all assemblies, or nothing to scan all assembly</param>
        /// <param name="helpFormatter">Help formatting engine, or nothing to format the messages to console</param>
        /// <param name="configurationBuilder">Configuration builder action, or nothing to use defaults</param>
        /// <returns>Results from the operation</returns>
        public static Operation.OperationExecutionResult Execute(string[] args, Type type, IHelpFormatter helpFormatter = null, Action<Parser.Configuration> configurationBuilder = null)
        {
            return Execute(args, new[] { type }, helpFormatter, configurationBuilder);
        }

        /// <summary>
        /// Executes the command passed in the arguments
        /// </summary>
        /// <param name="args">List of retrieved arguments, or null to extract the arguments from the Environment</param>
        /// <param name="types">List of types in which to get all assemblies, or nothing to scan all assembly</param>
        /// <param name="helpFormatter">Help formatting engine, or nothing to format the messages to console</param>
        /// <param name="configurationBuilder">Configuration builder action, or nothing to use defaults</param>
        /// <returns>Results from the operation</returns>
        public static Operation.OperationExecutionResult Execute(string[] args, Type[] types, IHelpFormatter helpFormatter = null, Action<Parser.Configuration> configurationBuilder = null)
        {
            // Get safe args collection
            args = GetSafeArgs(args);

            // Extract available commands based on information gathered
            var configuration = CommandParser.Parse(types, configurationBuilder);

            // Start logging operation
            var operationResult = new Operation.OperationExecutionResult();

            // Get command from arguments
            operationResult.Messages.Add(new Operation.Messages.Progress(Resources.GetFromArguments));
            var command = configuration.GetCommand(args);
            var requestedHelp = configuration.GetArgumentHelp(args);
            operationResult.Messages.Add(new Operation.Messages.Information(String.Format(Resources.CommandInformation, (command == null ? "Unknown" : command.Name), requestedHelp)));

            // User requested help
            if (requestedHelp)
            {
                CommandParser.PrintHelpInternal(configuration, command, helpFormatter, operationResult.Messages, false);
                return operationResult;
            }

            // We could not get a command, but there is only one that exists, so we can try to execute it
            if (command == null && configuration.Commands.Count() == 1)
            {
                command = configuration.Commands.First();
                operationResult.Messages.Add(new Operation.Messages.Information(String.Format(Resources.UnknownCommandSingle, command.Name)));
                command.ExecuteInternal(args, operationResult, helpFormatter);
                return operationResult;
            }

            // Otherwise, execute from arguments
            if (command != null)
            {
                operationResult.Messages.Add(new Operation.Messages.Information(String.Format(Resources.ExecutingCommandFromArguments, command.Name)));
                command.ExecuteInternal(args, operationResult, helpFormatter);
                return operationResult;
            }
            else
            {
                operationResult.Messages.Add(new Operation.Messages.Error(Resources.UnknownCommand));
                CommandParser.PrintHelpInternal(configuration, null, helpFormatter, operationResult.Messages);
                return operationResult;
            }
        }

        #region Internal Functions

        /// <summary>
        /// Gets a safe args collection
        /// </summary>
        /// <param name="args">Input args, which can be null</param>
        /// <returns>Output args, which will be an empty array if null</returns>
        internal static string[] GetSafeArgs(string[] args)
        {
            // In case no arguments are specified, we will get them from Environment class
            if (args == null)
            {
                args = new string[Environment.GetCommandLineArgs().Length - 1];
                new List<string>(Environment.GetCommandLineArgs()).CopyTo(1, args, 0, Environment.GetCommandLineArgs().Length - 1);
            }

            // Remove empty args
            return args.Where(i => !String.IsNullOrEmpty(i)).ToArray();
        }

        #endregion
    }
}
