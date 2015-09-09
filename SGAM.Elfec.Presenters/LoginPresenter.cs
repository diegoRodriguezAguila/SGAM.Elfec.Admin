using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGAM.Elfec.Presenters
{
    public class LoginPresenter : BasePresenter<ILoginView>
    {
        public LoginPresenter(ILoginView view) : base(view)
        {
        }
    }
}
