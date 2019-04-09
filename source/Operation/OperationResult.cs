
namespace CommandLineEngine.Operation
{
    /// <summary>
    /// Represents the operation results
    /// </summary>
    public sealed class OperationResult
    {
        #region Class Construction

        /// <summary>
        /// Creates the operation results
        /// </summary>
        public OperationResult()
        {
            this.Valid = true;
            this.Messages = new Items(this);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Override default string representation
        /// </summary>
        /// <returns>returns the list of messages</returns>
        public override string ToString()
        {
            return Valid ? (Output.ToString() ?? Resources.Valid) : Resources.Invalid;
        }


        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets if the operation is valid
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// Gets the output from execution
        /// </summary>
        public object Output { get; internal set; }

        /// <summary>
        /// Gets the operation messages
        /// </summary>
        public Items Messages { get; private set; }

        #endregion
    }
}
