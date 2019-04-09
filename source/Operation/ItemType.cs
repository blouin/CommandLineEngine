
namespace CommandLineEngine.Operation
{
    /// <summary>
    /// All possible item types
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// Error item
        /// </summary>
        Error = 1,

        /// <summary>
        /// Warning item
        /// </summary>
        Warning = 2,

        /// <summary>
        /// Information item
        /// </summary>
        Information = 4,

        /// <summary>
        /// Progress item
        /// </summary>
        Progress = 8,
    }
}
