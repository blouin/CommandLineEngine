
namespace CommandLineEngine.Operation.Messages
{
    /// <summary>
    /// Used to report progress
    /// </summary>
    public class Progress : Item
    {
        #region Class Construction

        /// <summary>
        /// Used to report progress
        /// </summary>
        /// <param name="message">Progress message</param>
        public Progress(string message)
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
            get { return ItemType.Progress; }
        }

        #endregion
    }
}
