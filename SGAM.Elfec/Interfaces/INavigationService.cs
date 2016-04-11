using SGAM.Elfec.Presenters.Presentation.Collections;
using System.Windows.Controls;

namespace SGAM.Elfec.Interfaces
{
    public interface INavigationService
    {
        ObservableStack<Control> NavigationStack { get; set; }

        Control Current { get; }

        /// <summary>
        /// Adds an element to the navigation stack. It deletes
        /// any forwarding element if the control type is different from 
        /// the forwarding element of current view
        /// </summary>
        /// <param name="control"></param>
        void Add(Control control);

        /// <summary>
        /// Goes back in the navigation stack (doesn't delete the forwarding elements)
        /// </summary>
        void Back();

        /// <summary>
        /// Goes forward in the navigation stack (doesn't delete any elements)
        /// </summary>
        void Forward();

        /// <summary>
        /// Goes to the specified Index in the navigation stack (doesn't delete any elements)
        /// </summary>
        /// <param name="index"></param>
        void NavigateTo(int index);
        /// <summary>
        /// Clears the navigation stack
        /// </summary>
        void Clear();
    }
}
