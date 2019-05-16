using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CommandLineEngine.Operation
{
    /// <summary>
    /// A collection of reported items
    /// </summary>
    public sealed class Items : IEnumerable<Item>
    {
        #region Class Construction

        /// <summary>
        /// A collection of reported items
        /// </summary>
        /// <param name="owner">A reference to owning class</param>
        public Items(OperationResult owner)
        {
            this.Owner = owner;
            this.List = new List<Item>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Adds an item to the collection
        /// </summary>
        /// <param name="errorMessage">Error message to add</param>
        public void Add(string errorMessage)
        {
            Add(new Messages.Error(errorMessage));
            Owner.Valid = false;
        }

        /// <summary>
        /// Adds an item to the collection
        /// </summary>
        /// <param name="item">Message item to ass</param>
        public void Add(Item item)
        {
            List.Add(item);
        }

        /// <summary>
        /// Inserts an item to the collection
        /// </summary>
        /// <param name="index">Index to insert at</param>
        /// <param name="errorMessage">Error message to insert</param>
        public void Insert(int index, string errorMessage)
        {
            Insert(index, new Messages.Error(errorMessage));
            Owner.Valid = false;
        }

        /// <summary>
        /// Inserts an item to the collection
        /// </summary>
        /// <param name="index">Index to insert at</param>
        /// <param name="item">Message item to insert</param>
        public void Insert(int index, Item item)
        {
            List.Insert(index, item);
        }

        /// <summary>
        /// Specified an action to execute on all items in the collection
        /// </summary>
        /// <param name="action">Action to perform</param>
        public void ForEach(Action<Item> action)
        {
            List.ForEach(action);
        }

        /// <summary>
        /// Clears all the items in the collection
        /// </summary>
        public void Clear()
        {
            List.Clear();
            Owner.Valid = true;
        }

        /// <summary>
        /// Output all the messages in a single object
        /// </summary>
        /// <returns>String representation of message, split by a line break</returns>
        public override string ToString()
        {
            return String.Join(Environment.NewLine, List.Select(m => m.Message).ToArray());
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the owning class, used to set to false if we insert error messages
        /// </summary>
        internal OperationResult Owner { get; private set; }

        /// <summary>
        /// Gets the actual list of messages
        /// </summary>
        internal List<Item> List { get; private set; }

        #endregion

        #region IEnumerator Members

        /// <summary>
        /// Gets the enumerator over the items
        /// </summary>
        /// <returns>Enumerator object</returns>
        public IEnumerator<Item> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        /// <summary>
        /// Gets the enumerator over the items
        /// </summary>
        /// <returns>Enumerator object</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}