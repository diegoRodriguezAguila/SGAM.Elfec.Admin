﻿using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Commands;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Presenters.Presentation.Collections;
using SGAM.Elfec.Presenters.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

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
        private ObservableCollection<User> _filteredUsers;
        private UserGroup _selectedUserGroup;
        private string _searchQuery;
        private ListSearcher<User> _listSearcher;

        #endregion

        #region Properties

        public UserGroup SelectedUserGroup
        {
            get { return _selectedUserGroup; }
            set
            {
                _selectedUserGroup = value;
                if (_selectedUserGroup != null)
                {
                    FilteredUsers = _selectedUserGroup.Members?.ToObservableCollectionAsync();
                    _listSearcher = ListSearcher<User>.For(_selectedUserGroup.Members)
                        .With(UserMatchHelper.MatchesSearchQuery);
                    _listSearcher.SearchCompleted += SearchCompleted;
                }
                RaisePropertyChanged("SelectedUserGroup");
            }
        }

        public ObservableCollection<UserGroup> UserGroups
        {
            get { return _userGroups; }
            set
            {
                _userGroups = value;
                SelectedUserGroup = _userGroups.FirstOrDefault();
                RaisePropertyChanged("UserGroups");
            }
        }

        public ObservableCollection<User> FilteredUsers
        {
            get { return _filteredUsers; }
            set
            {
                _filteredUsers = value;
                RaisePropertyChanged("FilteredUsers");
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

        #endregion

        #region Commands

        public ICommand ShowUserDetailsCommand
        {
            get { return new ListItemCommand<User>((user) => { View.ShowUserDetails(user); }); }
        }

        public ICommand DismissUserGroupCommand
        {
            get { return new ListItemCommand<UserGroup>(DismissUserGroup); }
        }

        public ICommand EnableUserGroupCommand
        {
            get { return new ListItemCommand<UserGroup>(EnableUserGroup); }
        }

        public ICommand SearchUsersCommand => new DelegateCommand(SearchUsers);

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
                    (error) => { View.OnLoadingErrors(isRefresh, error); });
        }

        /// <summary>
        /// Da de baja un grupo de usuarios
        /// </summary>
        /// <param name="userGroup">grupo de usuarios</param>
        private void DismissUserGroup(UserGroup userGroup)
        {
            View.ProcessingStatusChange();
            UserGroupManager.UpdateUserGroupStatus(userGroup.Id, UserGroupStatus.Disabled)
                .ObserveOnDispatcher()
                .Subscribe(
                    (updatedUserGroup) =>
                    {
                        RefreshUpatedStatus(userGroup, updatedUserGroup);
                        View.StatusChanged();
                    }, View.ErrorChangingStatus);
        }


        /// <summary>
        /// Da de alta un grupo de usuarios
        /// </summary>
        /// <param name="userGroup">grupo de usuarios</param>
        private void EnableUserGroup(UserGroup userGroup)
        {
            View.ProcessingStatusChange();
            UserGroupManager.UpdateUserGroupStatus(userGroup.Id, UserGroupStatus.Enabled)
                .ObserveOnDispatcher()
                .Subscribe(
                    (updatedUserGroup) =>
                    {
                        RefreshUpatedStatus(userGroup, updatedUserGroup);
                        View.StatusChanged();
                    }, View.ErrorChangingStatus);
        }

        private void RefreshUpatedStatus(UserGroup userGroup, UserGroup updatedUserGroup)
        {
            //TODO look for Move instead of all this stuff, with binary search
            UserGroups.Remove(userGroup);
            updatedUserGroup.Members = userGroup.Members;
            UserGroups.Add(updatedUserGroup);
            UserGroups = UserGroups.OrderByDescending(u => u.Status)
                .ThenBy(u => u.Name).ToObservableCollectionAsync();
        }

        private void SearchUsers()
        {
            _listSearcher?.Search(SearchQuery);
        }

        private void SearchCompleted(object sender, IEnumerable<User> users)
        {
            FilteredUsers?.Clear();
            FilteredUsers = users.ToObservableCollectionAsync();
        }

        #endregion
    }
}