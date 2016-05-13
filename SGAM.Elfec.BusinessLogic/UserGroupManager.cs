using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Security;
using System.Collections.Generic;

namespace SGAM.Elfec.BusinessLogic
{
    public class UserGroupManager
    {
        /// <summary>
        /// Registra un grupo de usuarios en la api
        /// </summary>
        /// <param name="userGroup">grupo de usuarios a registrar</param>
        /// <param name="callback">callback</param>
        public static void RegisterUserGroup(UserGroup userGroup, ResultCallback<UserGroup> callback)
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            RestInvoker.InvokeWebService(callback, RestEndpointFactory
                    .Create<IUserGroupsEndpoint>(user.Username, user.AuthenticationToken)
                    .RegisterUserGroup(userGroup,
                    userGroup.Members.ToString((u) => { return u.Username; })));
        }

        /// <summary>
        /// Agrega miembros al grupo con el Id especificado
        /// </summary>
        /// <param name="userGroupId"></param>
        /// <param name="members"></param>
        /// <param name="callback"></param>
        public static void AddMembers(string userGroupId, IList<User> members, VoidCallback callback)
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            RestInvoker.InvokeWebService(callback, RestEndpointFactory
                    .Create<IUserGroupsEndpoint>(user.Username, user.AuthenticationToken)
                    .AddMembers(userGroupId,
                    members.ToString((u) => { return u.Username; })));
        }
    }
}
