using SGAM.Elfec.Interfaces;
using SGAM.Elfec.Presenters;
using SGAM.Elfec.Presenters.Presentation.Collections;
using System;
using System.Windows.Controls;

namespace SGAM.Elfec.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        internal NavigationService()
        {
            NavigationStack = new ObservableStack<Control>();
            _current = 0;
        }

        public ObservableStack<Control> NavigationStack { get; set; }
        private int _current;


        public Control Current
        {
            get
            {
                if (NavigationStack.Count > 0)
                    return NavigationStack[_current];
                return null;
            }
        }

        /// <summary>
        /// Adds an element to the navigation stack. It deletes
        /// any forwarding element if the control type is different from 
        /// the forwarding element of current view
        /// </summary>
        /// <param name="control"></param>
        public void Add(Control control)
        {
            if (NavigationStack.Count > 0 && _current > 0 &&
                control.GetType() == NavigationStack.Peek().GetType())
            {
                for (int i = 0; i < _current; i++)
                {
                    NavigationStack.Pop();
                }
                _current = 0;
            }
            NavigationStack.Push(control);
            RaisePropertyChanged("Current");
        }

        /// <summary>
        /// Goes back in the navigation stack (doesn't delete the forwarding elements)
        /// </summary>
        public void Back()
        {
            if (_current < NavigationStack.Count - 1)
            {
                _current++;
                RaisePropertyChanged("Current");
            }
        }

        /// <summary>
        /// Goes forward in the navigation stack (doesn't delete any elements)
        /// </summary>
        public void Forward()
        {
            if (_current > 0)
            {
                _current--;
                RaisePropertyChanged("Current");
            }
        }

        /// <summary>
        /// Goes to the specified Index in the navigation stack (doesn't delete any elements)
        /// </summary>
        /// <param name="index"></param>
        public void NavigateTo(int index)
        {
            if (index < 0 || index >= NavigationStack.Count)
                throw new ArgumentOutOfRangeException("index is invalid");
            _current = index;
            RaisePropertyChanged("Current");
        }

        /// <summary>
        /// Clears the navigation stack
        /// </summary>
        public void Clear()
        {
            _current = 0;
            NavigationStack.Clear();
            RaisePropertyChanged("Current");
        }

    }
}
