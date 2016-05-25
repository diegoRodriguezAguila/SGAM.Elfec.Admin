using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Presenters.Presentation.Collections;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
namespace SGAM.Elfec.Presenters
{
    public class ShowUsersPresenter : BasePresenter<IShowUsersView>
    {
        public ShowUsersPresenter(IShowUsersView view) : base(view)
        {
            LoadAllUserGroups();
        }

        #region Private Attributes
        private ObservableCollection<UserGroup> _userGroups;
        private UserGroup _selectedUserGroup;
        #endregion

        #region Properties
        public UserGroup SelectedUserGroup
        {
            get { return _selectedUserGroup; }
            set
            {
                _selectedUserGroup = value;
                RaisePropertyChanged("SelectedUserGroup");
            }
        }
        public ObservableCollection<UserGroup> UserGroups
        {
            get { return _userGroups; }
            set
            {
                _userGroups = value;
                SelectedUserGroup = _userGroups.First();
                RaisePropertyChanged("UserGroups");
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Realiza la carga de todos los usuarios, ya sea por webservices o de la caché
        /// </summary>
        /// <param name="isRefresh"></param>
        private void LoadAllUserGroups(bool isRefresh = false)
        {
            View.OnLoadingData(isRefresh);
            UserGroupManager.GetAllUserGroups(true)
                .Subscribe(
                (userGroups) =>
                {
                    UserGroups = userGroups.ToObservableCollectionAsync();
                    View.OnDataLoaded();
                },
                (error) =>
                {
                    View.OnLoadingErrors(isRefresh, error);
                });
        }
        #endregion
    }
}
