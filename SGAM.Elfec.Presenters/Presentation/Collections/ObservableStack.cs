using System;
using System.Collections.ObjectModel;

namespace SGAM.Elfec.Presenters.Presentation.Collections
{
    /// <summary>
    /// Observable stack usable by wpf 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObservableStack<T> : ObservableCollection<T>
    {
        /// <summary>
        /// Gets the first element in the stack, without modifying it
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Stack is empty");
            return this[0];
        }

        /// <summary>
        /// Gets the first element in the stack and removes it
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (Count == 0) throw new InvalidOperationException("Stack is empty");
            var head = this[0];
            RemoveAt(0);
            return head;
        }

        /// <summary>
        /// Push an item to the stack
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            Insert(0, item);
        }
    }
}
