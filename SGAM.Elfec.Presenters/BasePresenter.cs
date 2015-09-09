using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGAM.Elfec.Presenters
{
    /// <summary>
    /// Base Presenter Interface
    /// </summary>
    /// <typeparam name="TView"></typeparam>
    public abstract class BasePresenter<TView> where TView : IBaseView
    {
        /// <summary>
        /// The View abstraction
        /// </summary>
        protected TView View { get; set; }

        ///<summary>
        ///Set or attach the view to this presenter
        ///</summary>
        public BasePresenter(TView view)
        {
            View = view;
        }
        
    }
}
