using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandLineEngine.Attributes;
using CommandLineEngine.Operation;

namespace CommandLineEngine.Parser
{
    /// <summary>
    /// Represents a single executable command
    /// </summary>
    public sealed class Command
    {
        #region Class Construction

        /// <summary>
        /// Represents a single executable command
        /// </summary>
        /// <param name="configuration">Reference to full configuration</param>
        /// <param name="methodInfo">Reference to the method info from reflection</param>
        /// <param name="commandAttribute">Extracted command attribute</param>
        internal Command(Configuration configuration, MethodInfo methodInfo, CommandAttribute commandAttribute)
        {
            this.Configuration = configuration;
            this.MethodInfo = methodInfo;

            // Set values
            this.Name = !String.IsNullOrEmpty(commandAttribute.Name) ? commandAttribute.Name : methodInfo.Name;
            this.Description = commandAttribute.Description;
            this.HelpUrl = commandAttribute.HelpUrl;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Validates the configuration
        /// </summary>
        /// <param name="operationResult">Operation result to write in</param>
        internal void ValidateConfigurationInternal(OperationResult operationResult)
        {
            var parameterLongNames = Parameters.Select(i => i.Name.ToLower());
            var parameterShortNames = Parameters.Where(i => !String.IsNullOrEmpty(i.ShortName)).Select(i => i.ShortName.ToLower());
            var parameterNames = parameterLongNames.Concat(parameterShortNames);

            // Ensure all parameters have distinct names
            var groups = parameterNames.ToLookup(i => i);
            var duplicateGroups = groups.Where(i => i.Count() > 1);
            duplicateGroups.ForEach(i => operationResult.Messages.Add(String.Format(Resources.ParameterDuplicate, i.Key, Name)));

            // Ensure no parameter has a space in it
            var spacedParameters = parameterNames.Where(i => i.Contains(" "));
            spacedParameters.ForEach(i => operationResult.Messages.Add(String.Format(Resources.ParameterCanNotHaveSpace, i, Name)));

            // Ensure no command has a reserved keyword
            if (parameterNames.Any(i => Configuration.HelpCommandNames.Any(j => String.Compare(j, i, true) == 0)))
            {
                operationResult.Messages.Add(String.Format(Resources.ParameterReservedKeywords, String.Join("', '", Configuration.HelpCommandNames), Name));
            }

            // Ensure no hidden parameter is required
            var hiddenInvalid = Parameters.Where(i => !i.Visible && !i.HasDefaultValue);
            hiddenInvalid.ForEach(i => operationResult.Messages.Add(String.Format(Resources.ParameterVisible, i,Name, Name)));
        }

        /// <summary>
        /// Validates the command before executing
        /// </summary>
        /// <param name="args">List of arguments</param>
        /// <param name="operationResult">Operation result to write in</param>
        internal InputArguments ValidateInternal(string[] args, OperationResult operationResult)
        {
            operationResult.Messages.Add(new Operation.Types.Progress(String.Format(Resources.ValidatingCommand, Name)));

            try
            {
                // Parse the arguments
                var parsedArguments = new InputArguments(this, args, operationResult);
                if (!parsedArguments.ParseValid)
                {
                    operationResult.Messages.Add(Resources.ParametersNotParsable);
                    return parsedArguments;
                }

                // Validate the parameters
                foreach (var p in Parameters)
                {
                    // Check that the value was recieved
                    var v = parsedArguments.GetValue(p);
                    if (v == null && !(p.HasDefaultValue && p.DefaultValue == null))
                    {
                        operationResult.Messages.Add(String.Format(Resources.ParameterMissing, p.Name));
                    }

                    // Check that the custom rules are applied
                    p.Rules.ForEach(i => operationResult.Valid &= i.Evaluate(parsedArguments, operationResult));
                }

                // For information
                if (operationResult.Valid)
                {
                    operationResult.Messages.Add(new Operation.Types.Information(String.Format(Resources.CommandValid, Name)));
                }
                else
                {
                    operationResult.Messages.Add(new Operation.Types.Warning(String.Format(Resources.CommandInvalid, Name)));
                }

                return parsedArguments;
            }
            catch (Exception e)
            {
                throw new CommandLineEngineException(Resources.ErrorValidatingCommand, operationResult, e);
            }
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="args">List of arguments</param>
        /// <param name="operationResult">Operation result to write in</param>
        /// <param name="helpFormatter">Formatter object in case we need to write help</param>
        internal void ExecuteInternal(string[] args, OperationExecutionResult operationResult, IHelpFormatter helpFormatter = null)
        {
            var parsedArguments = ValidateInternal(args, operationResult);
            if (!operationResult.Valid)
            {
                CommandParser.PrintHelpInternal(Configuration, this, helpFormatter, operationResult.Messages);
                return;
            }

            try
            {
                operationResult.Messages.Add(new Operation.Types.Progress(String.Format(Resources.ExecutingCommand, Name)));

                // Create instance of reflected type
                var target = (object)null;
                if (!MethodInfo.IsStatic)
                {
                    // No check, if it crashes it crashes
                    operationResult.Messages.Add(new Operation.Types.Warning(String.Format(Resources.CreatingDefaultInstance, MethodInfo.DeclaringType)));
                    target = Activator.CreateInstance(MethodInfo.DeclaringType, true);
                }

                // Get values
                var allParameters = SystemParameters.Concat(Parameters).OrderBy(i => i.ParameterInfo.Position);
                var values = allParameters.Select(i =>
                    {
                        if (typeof(InputArguments).GetTypeInfo().IsAssignableFrom(i.ParameterInfo.ParameterType))
                        {
                            return parsedArguments;
                        }
                        else if (typeof(OperationResult).GetTypeInfo().IsAssignableFrom(i.ParameterInfo.ParameterType))
                        {
                            return operationResult;
                        }
                        else if (typeof(Array).GetTypeInfo().IsAssignableFrom(i.ParameterInfo.ParameterType))
                        {
                            var finalType = i.ParameterInfo.ParameterType.GetElementType();
                            var objectArray = parsedArguments.GetValue(i)
                                .Select(j => Convert.ChangeType(j, finalType))
                                .ToArray();
                            var arr = Array.CreateInstance(finalType, objectArray.Length);
                            Array.Copy(objectArray, arr, objectArray.Length);
                            return arr;
                        }
                        else
                        {
                            return Convert.ChangeType(parsedArguments.GetValue(i)[0], i.ParameterInfo.ParameterType);
                        }
                    })
                    .ToArray();

                operationResult.Output = MethodInfo.Invoke(target, values);
            }
            catch (Exception e)
            {
                throw new CommandLineEngineException(Resources.ErrorExecutingCommand, operationResult, e);
            }
            finally
            {
                CommandParser.PrintDebug(operationResult.Messages);
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a string representation of the command
        /// </summary>
        /// <returns>String representation</returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Gets the command parameter by name
        /// </summary>
        /// <param name="name">Name to try and match</param>
        /// <returns>Matching argument</returns>
        public CommandParameter GetParameter(string name)
        {
            return Parameters.FirstOrDefault(i => i.IsCorrectParameter(name));
        }

        /// <summary>
        /// Validates the command before executing
        /// </summary>
        /// <param name="args">List of arguments</param>
        /// <returns>Results from validation</returns>
        public OperationResult Validate(string[] args)
        {
            var operationResult = new Operation.OperationResult();
            ValidateInternal(args, operationResult);
            return operationResult;
        }

        /// <summary>
        /// Executes the command
        /// </summary>
        /// <param name="args">List of arguments</param>
        /// <param name="operationResult">Operation result to write in</param>
        /// <param name="helpFormatter">Formatter object in case we need to write help</param>
        /// <returns>Results from execution</returns>
        public OperationExecutionResult Execute(string[] args, IHelpFormatter helpFormatter = null)
        {
            var operationResult = new Operation.OperationExecutionResult();
            ExecuteInternal(args, operationResult);
            return operationResult;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets a reference to full configuration
        /// </summary>
        internal Configuration Configuration { get; private set; }
        
        /// <summary>
        /// Gets a reference to the method info from reflection
        /// </summary>
        internal System.Reflection.MethodInfo MethodInfo { get; private set; }

        /// <summary>
        /// Gets the list of system parameters for the command
        /// </summary>
        internal IEnumerable<CommandParameter> SystemParameters { get; set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the command name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the command description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the URL for more help
        /// </summary>
        public string HelpUrl { get; private set; }

        /// <summary>
        /// Gets the list of parameters for the command
        /// </summary>
        public IEnumerable<CommandParameter> Parameters { get; internal set; }

        #endregion
    }
}
