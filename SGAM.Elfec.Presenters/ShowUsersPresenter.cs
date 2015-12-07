using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace SGAM.Elfec.Presenters
{
    public class ShowUsersPresenter : BasePresenter<IShowUsersView>
    {
        public ShowUsersPresenter(IShowUsersView view) : base(view)
        {
            LoadAllUsers();
        }

        #region Private Attributes
        private ObservableCollection<User> _users;
        #endregion

        #region Properties
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                RaisePropertyChanged("Users");
            }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Realiza la carga de todos los usuarios, ya sea por webservices o de la caché
        /// </summary>
        /// <param name="isRefresh"></param>
        public void LoadAllUsers(bool isRefresh = false)
        {
            new Thread(() =>
            {
                View.OnLoadingData(isRefresh);
                var callback = new ResultCallback<IList<User>>();
                callback.Success += (s, users) =>
                {
                    Users = new ObservableCollection<User>(users);
                    View.OnDataLoaded();
                };
                callback.Failure += (s, errors) =>
                {
                    View.OnLoadingErrors(isRefresh, errors);
                };
                UsersManager.GetAllUsers(callback);
            }).Start();
        }
        #endregion
    }
}
