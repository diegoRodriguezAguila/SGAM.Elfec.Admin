using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SGAM.Elfec.Presenters
{
    /// <summary>
    /// Presenter para la vista de login
    /// </summary>
    public class LoginPresenter : BasePresenter<ILoginView>
    {
        public LoginPresenter(ILoginView view) : base(view)
        {
        }

        /// <summary>
        /// Inicia el proceso de Login
        /// </summary>
        public void Login()
        {
            new Thread(() =>
            {
                View.ShowWaiting();


            }).Start();
        }
    }
}
