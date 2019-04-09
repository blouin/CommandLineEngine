using System;

namespace CommandLineEngine.Attributes
{
    /// <summary>
    /// Attribute used to apply custom rules to parameter before execution
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true, Inherited = true)]
    public abstract class ParameterRuleAttribute : Attribute
    {
        #region Class Construction

        /// <summary>
        /// Attribute used to apply custom rules to parameter before execution
        /// </summary>
        protected ParameterRuleAttribute()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Evaluates the rule before execution
        /// </summary>
        /// <param name="inputArguments">Parsed input arguments</param>
        /// <param name="operationResult">Operation message to write in</param>
        /// <returns>True if successful evaluation, otherwise false</returns>
        public abstract bool Evaluate(Parser.InputArguments inputArguments, Operation.OperationResult operationResult);

        #endregion
    }
}
