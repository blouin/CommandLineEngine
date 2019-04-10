using System;

namespace CommandLineEngine
{
    /// <summary>
    /// Exception thrown when an error occurs either validating or executing a command. 
    /// </summary>
    public class CommandLineEngineException : Exception
    {
        #region Class Construction

        /// <summary>
        /// Exception thrown when an error occurs either validating or executing a command. 
        /// </summary>
        /// <param name="operationResult">Operation results</param>
        /// <param name="innerException">Inner exception</param>
        public CommandLineEngineException(string message, Operation.OperationResult operationResult, Exception innerException)
           : base(message, innerException)
        {
            this.OperationMessages = operationResult.Messages;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the full detail of messages
        /// </summary>
        public Operation.Items OperationMessages { get; private set; }

        #endregion
    }
}
