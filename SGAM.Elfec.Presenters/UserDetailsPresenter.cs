using SGAM.Elfec.Model;
using SGAM.Elfec.Presenters.Views;

namespace SGAM.Elfec.Presenters
{
    public class UserDetailsPresenter : BasePresenter<IUserDetailsView>
    {
        public UserDetailsPresenter(IUserDetailsView view, User user) : base(view)
        {
            User = user;
        }

        #region Private Attributes
        private User _user;
        #endregion

        #region Properties
        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged("User");
            }
        }

        #endregion
    }
}
