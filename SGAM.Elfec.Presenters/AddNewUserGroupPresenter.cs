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
            Members = new ObservableCollection<User>();
            LoadElegibleUsers();
        }



        #region Private Attributes
        private ObservableCollection<User> _elegibleUsers;
        private User _memberToAdd;
        private ObservableCollection<User> _members;
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

        public ObservableCollection<User> Members
        {
            get { return _members; }
            set
            {
                _members = value;
                RaisePropertyChanged("Members");
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

        private void AddMember()
        {
            if (MemberToAdd != null && !Members.Contains(MemberToAdd))
            {
                Members.Add(MemberToAdd);
                ElegibleUsers.Remove(MemberToAdd);
                MemberToAdd = null;
                UserFullName = null;
            }
        }

        private void DeleteMember(IList selectedMembers)
        {
            var membersToDel = selectedMembers.Cast<User>().ToList();
            if (membersToDel != null && membersToDel.Count > 0)
            {
                Members.RemoveRange(membersToDel);
                ElegibleUsers.AddRange(membersToDel);
                MemberToAdd = null;
                UserFullName = null;
            }
        }

    }
}
