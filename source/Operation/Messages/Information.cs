
namespace CommandLineEngine.Operation.Messages
{
    /// <summary>
    /// Used to report information
    /// </summary>
    public class Information : Item
    {
        #region Class Construction

        /// <summary>
        /// Used to report information
        /// </summary>
        /// <param name="message">Information message</param>
        public Information(string message)
            : base(message)
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the item type
        /// </summary>
        public override ItemType Type
        {
            get { return ItemType.Information; }
        }

        #endregion
    }
}
