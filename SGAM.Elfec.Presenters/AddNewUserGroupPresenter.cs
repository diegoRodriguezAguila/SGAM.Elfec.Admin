using SGAM.Elfec.BusinessLogic;
using SGAM.Elfec.Commands;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Presenters.Views;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace SGAM.Elfec.Presenters
{
    public class AddNewUserGroupPresenter : BasePresenter<IAddNewUserGroupView>
    {
        public AddNewUserGroupPresenter(IAddNewUserGroupView view)
            : base(view)
        {
            UserGroup = new UserGroup { Members = new ObservableCollection<User>() };
            LoadElegibleUsers();
        }



        #region Private Attributes
        private ObservableCollection<User> _elegibleUsers;
        private User _memberToAdd;
        private UserGroup _userGroup;
        private string _userFullName;
        #endregion

        #region Properties
        public ObservableCollection<User> ElegibleUsers
        {
            get { return _elegibleUsers; }
            set
            {
                _elegibleUsers = value;
                RaisePropertyChanged("ElegibleUsers");
            }
        }

        public User MemberToAdd
        {
            get { return _memberToAdd; }
            set
            {
                _memberToAdd = value;
                RaisePropertyChanged("MemberToAdd");
            }
        }

        public UserGroup UserGroup
        {
            get { return _userGroup; }
            set
            {
                _userGroup = value;
                RaisePropertyChanged("UserGroup");
            }
        }

        public string UserFullName
        {
            get { return _userFullName; }
            set
            {
                _userFullName = value;
                RaisePropertyChanged("UserFullName");
            }
        }

        #endregion

        #region Commands
        public ICommand AddMemberCommand { get { return new DelegateCommand(AddMember); } }
        public ICommand DeleteMemberCommand { get { return new ListItemCommand<IList>(DeleteMember); } }
        public ICommand RegisterUserGroupCommand { get { return new DelegateCommand(RegisterUserGroup); } }
        #endregion

        public bool FilterUsers(string search, object item)
        {
            // Cast the value to an User.
            User user = item as User;
            if (user != null)
            {
                if (user.FullName.ToLower().Contains(search.ToLower()))
                    return true;

                else if (user.Username.ToLower().Contains(search.ToLower()))
                    return true;
            }
            // If no match, return false.
            return false;
        }

        /// <summary>
        /// Carga todos los usuarios registrados elegibles para poder agregarlos al grupo
        /// </summary>
        private void LoadElegibleUsers()
        {
            new Thread(() =>
            {
                var callback = new ResultCallback<IList<User>>();
                callback.Success += (s, users) =>
                {
                    ElegibleUsers = new ObservableCollection<User>(users);
                };
                UsersManager.GetAllUsers(callback, UserStatus.Enabled);
            }).Start();
        }

        /// <summary>
        /// Adds a member to the user group
        /// </summary>
        private void AddMember()
        {
            if (MemberToAdd != null && !UserGroup.Members.Contains(MemberToAdd))
            {
                UserGroup.Members.Add(MemberToAdd);
                ElegibleUsers.Remove(MemberToAdd);
                MemberToAdd = null;
                UserFullName = null;
            }
        }

        /// <summary>
        /// Deletes the selected members from the user group
        /// </summary>
        /// <param name="selectedMembers"></param>
        private void DeleteMember(IList selectedMembers)
        {
            var membersToDel = selectedMembers.Cast<User>().ToList();
            if (membersToDel != null && membersToDel.Count > 0)
            {
                UserGroup.Members.RemoveRange(membersToDel);
                ElegibleUsers.AddRange(membersToDel);
                MemberToAdd = null;
                UserFullName = null;
            }
        }

        /// <summary>
        /// Registra el grupo de usuarios
        /// </summary>
        private void RegisterUserGroup()
        {
            new Thread(() =>
            {
                View.ShowRegisteringUserGroup();
                var callback = new ResultCallback<UserGroup>();
                callback.Success += (s, userGroup) =>
                {
                    RegisterMembers(userGroup);
                };
                callback.Failure += (s, errors) =>
                {
                    View.ShowRegistrationErrors(errors);
                };
                UserGroupManager.RegisterUserGroup(UserGroup, callback);
            }).Start();
        }

        /// <summary>
        /// Registra los miembros del grupo de usuarios
        /// </summary>
        /// <param name="userGroup">El grupo de usuarios</param>
        private void RegisterMembers(UserGroup userGroup)
        {
            new Thread(() =>
            {
                var callback = new VoidCallback();
                callback.Success += (s) =>
                {
                    View.ShowUserGroupRegistered(userGroup);
                };
                callback.Failure += (s, errors) =>
                {
                    View.ShowRegistrationErrors(errors);
                };
                UserGroupManager.AddMembers(userGroup.Id, UserGroup.Members, callback);
            }).Start();
        }

    }
}
