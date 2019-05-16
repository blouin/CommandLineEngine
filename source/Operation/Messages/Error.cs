
namespace CommandLineEngine.Operation.Messages
{
    /// <summary>
    /// Used to report an error
    /// </summary>
    public class Error : Item
    {
        #region Class Construction

        /// <summary>
        /// Used to report an error
        /// </summary>
        /// <param name="exception">Exception recieved</param>
        public Error(System.Exception exception)
            : this(exception.Message)
        {
            this.Exception = exception;
        }

        /// <summary>
        /// Used to report an error
        /// </summary>
        /// <param name="message">Error message</param>
        public Error(string message)
            : base(message)
        {
        }
        
        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the full exception
        /// </summary>
        public System.Exception Exception { get; internal set; }

        /// <summary>
        /// Gets the item type
        /// </summary>
        public override ItemType Type
        {
            get { return ItemType.Error; }
        }

        #endregion
    }
}
