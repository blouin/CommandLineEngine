using System;
using System.Collections.Generic;
using System.Linq;
using CommandLineEngine.Operation;
using CommandLineEngine.Operation.Types;

namespace CommandLineEngine.Parser
{
    /// <summary>
    /// Represents the input parameters
    /// </summary>
    public sealed class InputArguments
    {
        /// <summary>
        /// Seperator between argument name and value
        /// </summary>
        //public const char Separator = ':';

        #region Class Construction

        /// <summary>
        /// Creates the input arguments (parsing the array)
        /// </summary>
        /// <param name="command">Command we are running</param>
        /// <param name="args">Arguments in raw form, as recieved</param>
        /// <param name="operationResult">Operation result to write in</param>
        internal InputArguments(Command command, string[] args, OperationResult operationResult)
        {
            // Save values
            this.Command = command;
            this.ArgsRaw = CommandExecutor.GetSafeArgs(args);

            // Perform a mapping from short name to long name
            this.NameMap = Command.Parameters
                .Where(i => !String.IsNullOrEmpty(i.ShortName))
                .ToDictionary(i => i.ShortName.ToLower(), i => i.Name.ToLower());

            // Default mapping for parameters
            this.ArgsParsed = Command.Parameters
                .ToDictionary(i => i.GetFullLongName(), i => i.HasDefaultValue ? i.DefaultValue?.ToString() : null);

            // Perform parsing
            for (int i = 0; i < ArgsRaw.Length; i++)
            {
                if (Command.Configuration.IsArgument(ArgsRaw[i]))
                {
                    var possibleArgument = Command.GetArgument(ArgsRaw[i]);
                    var nextArgument = i + 1 < ArgsRaw.Length ? ArgsRaw[i + 1] : null;

                    //Ensure next argument is not another parameter
                    if (nextArgument != null && Command.Configuration.IsArgument(nextArgument))
                    {
                        nextArgument = null;
                    }

                    // Add to dictionary
                    AddArgument(possibleArgument, ArgsRaw[i], nextArgument);

                    // Skip one argument
                    if (nextArgument != null)
                    {
                        i++;
                    }
                }
                else
                {
                    if (i > 0 || String.Compare(args[i], Command.Name, true) != 0)
                    { 
                        operationResult.Messages.Add(new Warning(String.Format(Resources.UnknownArgument, args[i])));
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Adds or updates the argument value
        /// </summary>
        /// <param name="possibleArgument">Possible command parameter</param>
        /// <param name="key">Key in dictionary to add or update</param>
        /// <param name="value">Value to put in dictionary</param>
        private void AddArgument(CommandParameter possibleArgument, string key, string value)
        {
            // Adjust key
            key = possibleArgument?.GetFullLongName() ?? key;

            // Special case for boolean, which can have a default value of True
            if (value == null && possibleArgument?.ParameterInfo.ParameterType == typeof(bool))
            {
                value = bool.TrueString;
            }

            // Add or update
            ArgsParsed[key.ToLower()] = value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a string representation of the arguments
        /// </summary>
        /// <returns>Arguments as recieved</returns>
        public override string ToString()
        {
            return String.Join(" ", ArgsRaw);
        }

        /// <summary>
        /// Gets the value for the argument
        /// </summary>
        /// <param name="parameterName">Parameter name whose value to get</param>
        /// <returns>Value of the argument</returns>
        public string GetValue(string parameterName)
        {
            if (String.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentException(nameof(parameterName));
            }

            var possibleArgument = Command.GetArgument(parameterName);
            if (possibleArgument != null)
            { 
                return GetValue(possibleArgument);
            }
            else
            {
                return ArgsParsed.ContainsKey(parameterName) ? ArgsParsed[parameterName] : null;
            }
        }

        /// <summary>
        /// Gets the value for the argument
        /// </summary>
        /// <param name="parameter">Parameter whose value to get</param>
        /// <returns>Value of the argument</returns>
        public string GetValue(CommandParameter parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentException(nameof(parameter));
            }

            // Check in arguments parsed
            var longName = parameter?.GetFullLongName();
            var value = ArgsParsed.ContainsKey(longName) ? ArgsParsed[longName] : null;

            // Check in short names
            if (value == null)
            {
                var shortName = NameMap.ContainsKey(longName) ? NameMap[longName] : null;
                if (shortName != null)
                {
                    value = ArgsParsed.ContainsKey(shortName) ? ArgsParsed[shortName] : null;
                }
            }

            return value;
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Holds the raw arguments
        /// </summary>
        private string[] ArgsRaw { get; set; }

        /// <summary>
        /// Holds the parsed arguments
        /// </summary>
        private Dictionary<string, string> ArgsParsed { get; set; }

        /// <summary>
        /// Holds the mapping from short names to long names
        /// </summary>
        private Dictionary<string, string> NameMap { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the associated command
        /// </summary>
        public Command Command { get; private set; }

        #endregion
    }
}
