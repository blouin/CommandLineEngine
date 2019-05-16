
namespace CommandLineEngine.Operation.Messages
{
    /// <summary>
    /// Used to report warning
    /// </summary>
    public class Warning : Item
    {
        #region Class Construction

        /// <summary>
        /// Used to report warning
        /// </summary>
        /// <param name="message">Progress message</param>
        public Warning(string message)
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
            get { return ItemType.Warning; }
        }

        #endregion
    }
}
