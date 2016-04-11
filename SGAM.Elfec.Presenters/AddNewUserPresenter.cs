using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Presenters.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            _syncContext = SynchronizationContext.Current;
            _worker.DoWork += Filter;
            _worker.RunWorkerCompleted += FilterComplete;
            LoadNonRegisteredUsers();
        }

        #region Private Attributes
        private IList<User> _allUsers;
        private readonly SynchronizationContext _syncContext;
        private ObservableCollection<User> _users;
        private User _selectedUser;
        private string _searchQuery;
        private readonly BackgroundWorker _worker;
        private const int LOAD_FREQ = 25;
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
                    _allUsers = users;
                    DelayedBindUsers(users);
                    View.OnDataLoaded();
                };
                callback.Failure += (s, errors) =>
                {
                    View.OnLoadingErrors(isRefresh, errors);
                };
                UsersManager.GetAllUsers(callback, UserStatus.NonRegistered);
            }).Start();
        }

        /// <summary>
        /// Asigna la lista de Users con un delay para
        /// no sobrecargar el UI thread
        /// </summary>
        /// <param name="users"></param>
        private void DelayedBindUsers(IList<User> users)
        {
            Users = new ObservableCollection<User>(users.Take(LOAD_FREQ));
            new Thread(()=> {
                for (int i = 1; i * LOAD_FREQ < users.Count; i++)
                {
                    Thread.Sleep(80);
                    _syncContext.Post((o) => 
                    {
                        Users.AddRange(users.Skip(i * LOAD_FREQ).Take(LOAD_FREQ));
                    }, null);
                }}).Start();
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

            DelayedBindUsers(filtered);
        }

        #endregion
    }
}
