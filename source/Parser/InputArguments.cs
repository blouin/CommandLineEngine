using System;
using System.Collections.Generic;
using System.Linq;

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
        internal InputArguments(Command command, string[] args)
        {
            // Ensure args are valid
            args = CommandExecutor.GetSafeArgs(args);

            // Save values
            this.Command = command;
            this.ArgsRaw = args;

            // Perform a mapping from short name to long name
            this.NameMap = Command.Parameters
                .Where(i => !String.IsNullOrEmpty(i.ShortName))
                .ToDictionary(i => i.ShortName.ToLower(), i => i.Name.ToLower());

            // Perform parsing
            this.ArgsParsed = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; i++)
            {
                if (Command.Configuration.IsArgument(args[i]))
                {
                    var possibleArgument = Command.GetArgument(args[i]);
                    var nextArgument = i + 1 < args.Length ? args[i + 1] : null;

                    //Ensure next argument is not another parameter
                    if (nextArgument != null && Command.Configuration.IsArgument(nextArgument))
                    {
                        nextArgument = null;
                    }

                    // Add to dictionary
                    AddArgument(possibleArgument, args[i], nextArgument);

                    // Skip one argument
                    if (nextArgument != null)
                    {
                        i++;
                    }
                }
            }
        }

        private void AddArgument(CommandParameter possibleArgument, string key, string value)
        {
            // Adjust key
            key = possibleArgument?.GetFullLongName() ?? key;

            // Special case for boolean, which can have a default value of True
            if (value == null && possibleArgument?.ParameterInfo.ParameterType == typeof(bool))
            {
                value = bool.TrueString;
            }

            // Add item
            ArgsParsed.Add(key.ToLower(), value);
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

            // Check for default value
            if (value == null)
            {
                value = parameter.DefaultValue?.ToString();
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
