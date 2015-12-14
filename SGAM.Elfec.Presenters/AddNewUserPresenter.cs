using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;

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
        private string _searchQuery;
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

        public string SearchQuery
        {
            get { return _searchQuery; }
            set
            {
                _searchQuery = value;
                RaisePropertyChanged("SearchQuery");
            }
        }

        public ICommand RegisterUserCommand { get { return new DelegateCommand(RegisterUser); } }

        public ICommand SearchUserCommand { get { return new DelegateCommand(SearchUser); } }

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
                    //TODO find better way to bind this, without
                    //blocking UI thread
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

        /// <summary>
        /// Realiza el proceso de registro de usuario
        /// </summary>
        private void RegisterUser()
        {
            new Thread(() =>
            {
                View.ShowRegisteringUser();
                var callback = new ResultCallback<User>();
                callback.Success += (s, user) =>
                {
                    View.ShowUserRegisteredSuccessfully(user);
                };
                callback.Failure += (s, errors) =>
                {
                    View.ShowRegistrationErrors(errors);
                };
                UsersManager.RegisterUser(SelectedUser, callback);
            }).Start();
        }

        /// <summary>
        /// Inicia el filtro/búsuqeda de usuarios que coincidan con criterios
        /// </summary>
        private void SearchUser()
        {
            System.Console.WriteLine("SE LLAMO A SEARCH CHE: " + SearchQuery);
        }
        #endregion
    }
}
