using System;
using System.Linq;

namespace CommandLineEngine
{
    /// <summary>
    /// Exception thrown when an error occurs requiring developper intervention. 
    /// This occurs mainly when the configuration is invalid.
    /// </summary>
    public sealed class CommandLineEngineDevelopperException : CommandLineEngineException
    {
        #region Class Construction

        /// <summary>
        /// Exception thrown when an error occurs requiring developper intervention. 
        /// This occurs mainly when the configuration is invalid.
        /// </summary>
        /// <param name="operationResult">Operation results to output with the exception</param>
        public CommandLineEngineDevelopperException(Operation.OperationResult operationResult)
            : base(String.Format(Resources.DevelopperException, operationResult.Messages.First()), operationResult, null)
        {
        }

        #endregion
    }
}
