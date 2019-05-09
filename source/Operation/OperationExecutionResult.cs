
namespace CommandLineEngine.Operation
{
    /// <summary>
    /// Represents the operation results
    /// </summary>
    public class OperationExecutionResult : OperationResult
    {
        #region Class Construction

        /// <summary>
        /// Creates the operation results
        /// </summary>
        public OperationExecutionResult()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Override default string representation
        /// </summary>
        /// <returns>returns the list of messages</returns>
        public override string ToString()
        {
            return Valid ? (Output?.ToString() ?? Resources.Valid) : Resources.Invalid;
        }


        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the output from execution
        /// </summary>
        public object Output { get; internal set; }

        #endregion
    }
}
