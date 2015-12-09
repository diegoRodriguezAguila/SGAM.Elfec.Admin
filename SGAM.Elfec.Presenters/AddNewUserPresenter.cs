using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace SGAM.Elfec.Presenters
{
    public class AddNewUserPresenter : BasePresenter<IAddNewUserView>
    {
        public AddNewUserPresenter(IAddNewUserView view) : base(view)
        {
            LoadNonRegisteredUsers();
        }

        #region Private Attributes
        private ObservableCollection<User> _users;
        private User _selectedUser;
        #endregion

        #region Properties
        public ObservableCollection<User> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                SelectedUser = _users.FirstOrDefault();
                RaisePropertyChanged("Users");
            }
        }

        public User SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                RaisePropertyChanged("SelectedUser");
            }
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Carga todos los usuarios aun no registrados para poder agregarlos
        /// </summary>
        /// <param name="isRefresh"></param>
        private void LoadNonRegisteredUsers(bool isRefresh = false)
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
                UsersManager.GetAllUsers(callback, true);
            }).Start();
        }
        #endregion
    }
}
