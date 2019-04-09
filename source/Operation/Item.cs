using System;

namespace CommandLineEngine.Operation
{
    /// <summary>
    /// Base class for an item
    /// </summary>
    public abstract class Item
    {
        #region Class Construction

        /// <summary>
        /// Base class for an item
        /// </summary>
        /// <param name="message">Item message</param>
        protected Item(string message)
        {
            this.Message = message;
            this.CreationDate = DateTime.Now;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets a string representation for the progress item
        /// </summary>
        /// <returns>Item message</returns>
        public override string ToString()
        {
            return Message;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the item creation date
        /// </summary>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// Gets the progress message
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets the item type
        /// </summary>
        public abstract ItemType Type
        {
            get;
        }

        #endregion
    }
}
