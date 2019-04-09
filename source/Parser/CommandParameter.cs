using System;
using System.Collections.Generic;
using System.Reflection;
using CommandLineEngine.Attributes;

namespace CommandLineEngine.Parser
{
    /// <summary>
    /// Rerpresents a single command parameter
    /// </summary>
    public sealed class CommandParameter
    {
        #region Class Construction

        /// <summary>
        /// Rerpresents a single command parameter
        /// </summary>
        /// <param name="command">Reference to the command</param>
        /// <param name="parameterInfo">Reference to the paramter info from reflection</param>
        /// <param name="rules">List of paramter rules</param>
        /// <param name="parameterAttribute">Extracted parameter attribute</param>
        /// <param name="parameterHiddenAttribute">Extracted parameter hidden attribute</param>
        internal CommandParameter(Command command, ParameterInfo parameterInfo, IEnumerable<ParameterRuleAttribute> rules, ParameterAttribute parameterAttribute, ParameterHiddenAttribute parameterHiddenAttribute)
        {
            this.Command = command;
            this.ParameterInfo = parameterInfo;
            this.Rules = rules;

            // Set values
            this.Name = !String.IsNullOrEmpty(parameterAttribute?.Name) ? parameterAttribute.Name : parameterInfo.Name;
            this.ShortName = parameterAttribute?.ShortName;
            this.Description = parameterAttribute?.Description;
            this.Visible = parameterHiddenAttribute == null && ParameterInfo.ParameterType != typeof(InputArguments);
            this.DefaultValue = parameterInfo.DefaultValue;
            this.HasDefaultValue = parameterInfo.HasDefaultValue || ParameterInfo.ParameterType == typeof(InputArguments);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Gets the full long name
        /// </summary>
        /// <returns>Long name of argument</returns>
        internal string GetFullLongName()
        {
            return $"{Command.Configuration.LongParameterPrefix}{Name}".ToLower();
        }

        /// <summary>
        /// Gets the full short name
        /// </summary>
        /// <returns>Long name of argument</returns>
        internal string GetFullShortName()
        {
            return $"{Command.Configuration.ShortParameterPrefix}{ShortName}".ToLower();
        }

        /// <summary>
        /// Checks if the name is the actual parameter
        /// </summary>
        /// <param name="name">Name to match</param>
        /// <returns>True if correct parameter, otherwise false</returns>
        internal bool IsCorrectParameter(string name)
        {
            return String.Compare(name, GetFullLongName(), true) == 0 || String.Compare(name, GetFullShortName(), true) == 0;
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

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets a reference to the command
        /// </summary>
        internal Command Command { get; private set; }

        /// <summary>
        /// Gets a reference to the paramter info from reflection
        /// </summary>
        internal System.Reflection.ParameterInfo ParameterInfo { get; private set; }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the parameter name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the parameter short name
        /// </summary>
        public string ShortName { get; private set; }

        /// <summary>
        /// Gets the parameter description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets if the parameter should be visible in help formatters
        /// </summary>
        public bool Visible { get; private set; }

        /// <summary>
        /// Gets the default value for the paramter
        /// </summary>
        public object DefaultValue { get; private set; }

        /// <summary>
        /// Gets if the parameter has a default value
        /// </summary>
        public bool HasDefaultValue { get; private set; }

        /// <summary>
        /// Gets a list of paramter rules
        /// </summary>
        public IEnumerable<Attributes.ParameterRuleAttribute> Rules { get; private set; }

        #endregion
    }
}
