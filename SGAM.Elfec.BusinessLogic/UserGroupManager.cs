using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Security;
using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace SGAM.Elfec.BusinessLogic
{
    public class UserGroupManager
    {
        /// <summary>
        /// Obtiene todos los grupos de usuario, ya sea por medio de la caché o de webservices
        /// </summary>
        /// <param name="includeMembers">true si es que se quiere obtener las entidades
        /// de los miembros de cada grupo</param>
        /// <returns>Observable del resultado de la lista de grupos de usuario</returns>
        public static IObservable<IList<UserGroup>> GetAllUserGroups(bool includeMembers = false)
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            var parameters = new Dictionary<string, string>();
            parameters["sort"] = "-status,name";
            if (includeMembers)
                parameters["include"] = "members";
            return RestEndpointFactory.Create<IUserGroupsEndpoint>(user.Username, user.AuthenticationToken)
                .GetAllUserGroups(parameters).ToObservable()
                    .SubscribeOn(TaskPoolScheduler.Default)
                    .InterpretingErrors();
        }

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

        /// <summary>
        /// Actualiza el estado de un grupo de usuario, ya sea de baja o de alta
        /// </summary>
        /// <param name="userGroupId">id grupo de usuario</param>
        /// <param name="newStatus">nuevo estado para el grupo</param>
        /// <returns></returns>
        public static IObservable<UserGroup>
            UpdateUserGroupStatus(string userGroupId, UserGroupStatus newStatus)
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            return RestEndpointFactory.Create<IUserGroupsEndpoint>(user.Username, user.AuthenticationToken)
                .UpdateUserGroup(userGroupId, new UserGroup { Status = newStatus })
                .ToObservable()
                    .SubscribeOn(TaskPoolScheduler.Default)
                    .InterpretingErrors();
        }
    }
}
