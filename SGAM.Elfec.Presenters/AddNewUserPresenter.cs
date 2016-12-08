using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Presenters.Presentation.Collections;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Input;

namespace SGAM.Elfec.Presenters
{
    public class AddNewUserPresenter : BasePresenter<IAddNewUserView>
    {
        public AddNewUserPresenter(IAddNewUserView view) : base(view)
        {
            _worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };
            _worker.DoWork += Filter;
            _worker.RunWorkerCompleted += FilterComplete;
            LoadNonRegisteredUsers();
        }

        #region Private Attributes
        private IList<User> _allUsers;
        private ObservableCollection<User> _users;
        private User _selectedUser;
        private string _searchQuery;
        private readonly BackgroundWorker _worker;
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

        public ICommand RegisterUserCommand => new DelegateCommand(RegisterUser);

        public ICommand SearchUserCommand => new DelegateCommand(SearchUser);

        #endregion

        #region Private Methods
        /// <summary>
        /// Carga todos los usuarios aun no registrados para poder agregarlos
        /// </summary>
        /// <param name="isRefresh"></param>
        private void LoadNonRegisteredUsers(bool isRefresh = false)
        {
            View.OnLoadingData(isRefresh);
            UsersManager.GetAllUsers(UserStatus.NonRegistered)
             .ObserveOn(SynchronizationContext.Current)
             .Subscribe((users) =>
             {
                 _allUsers = users;
                 Users = users.ToObservableCollectionAsync();
                 View.OnDataLoaded();
             }, (e) => View.OnLoadingErrors(isRefresh, e));
        }


        /// <summary>
        /// Realiza el proceso de registro de usuario
        /// </summary>
        private void RegisterUser()
        {
            View.ShowRegisteringUser();
            UsersManager.RegisterUser(SelectedUser)
            .ObserveOn(SynchronizationContext.Current)
            .Subscribe(View.ShowUserRegisteredSuccessfully,
            View.ShowRegistrationError);
        }

        /// <summary>
        /// Inicia el filtro/búsuqeda de usuarios que coincidan con criterios
        /// </summary>
        private void SearchUser()
        {
            if (_worker.IsBusy)
            {
                if (!_worker.CancellationPending)
                {
                    _worker.RunWorkerCompleted += RefilterOnCompletion;
                    _worker.CancelAsync();
                }
            }
            else
            {
                _worker.RunWorkerAsync(SearchQuery);
            }
        }

        private void RefilterOnCompletion(object sender, RunWorkerCompletedEventArgs e)
        {
            _worker.RunWorkerCompleted -= RefilterOnCompletion;
            _worker.RunWorkerAsync(SearchQuery);
        }

        private void Filter(object sender, DoWorkEventArgs e)
        {
            var query = ((string)e.Argument).Replace("  ", " ").Trim().ToLowerInvariant();
            e.Result = string.IsNullOrWhiteSpace(query) ? _allUsers : _allUsers.Where((u) =>
            {
                return UserMatchHelper.MatchesSearchQuery(u, query);
            }).ToList();
        }

        private void FilterComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                return;

            var filtered = (IList<User>)e.Result;
            Users.Clear();
            Users = filtered.ToObservableCollectionAsync();
        }

        #endregion
    }
}
