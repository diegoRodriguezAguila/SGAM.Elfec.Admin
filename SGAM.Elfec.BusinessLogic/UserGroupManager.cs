using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Enums;
using SGAM.Elfec.Security;
using System;
using System.Collections.Generic;
using System.Reactive;
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
            var parameters = new Dictionary<string, string>();
            parameters["sort"] = "-status,name";
            if (includeMembers)
                parameters["include"] = "members";
            return RestEndpointFactory.Create<IUserGroupsEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                .GetAllUserGroups(parameters).ToObservable()
                    .SubscribeOn(TaskPoolScheduler.Default)
                    .InterpretingErrors();
        }

        /// <summary>
        /// Registra un grupo de usuarios en la api
        /// </summary>
        /// <param name="userGroup">grupo de usuarios a registrar</param>
        /// <param name="callback">callback</param>
        public static IObservable<UserGroup> RegisterUserGroup(UserGroup userGroup)
        {
            return RestEndpointFactory
                    .Create<IUserGroupsEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                    .RegisterUserGroup(userGroup,
                    userGroup.Members.ToString((u) => u.Username))
                    .ToObservable()
                    .SubscribeOn(TaskPoolScheduler.Default)
                    .InterpretingErrors();
        }

        /// <summary>
        /// Agrega miembros al grupo con el Id especificado
        /// </summary>
        /// <param name="userGroupId"></param>
        /// <param name="members"></param>
        /// <param name="callback"></param>
        public static IObservable<Unit> AddMembers(string userGroupId, IList<User> members)
        {
            return RestEndpointFactory
                    .Create<IUserGroupsEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                    .AddMembers(userGroupId,
                    members.ToString((u) => u.Username))
                    .ToObservable()
                    .SubscribeOn(TaskPoolScheduler.Default)
                    .InterpretingErrors();
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
            return RestEndpointFactory.Create<IUserGroupsEndpoint>(SessionManager
                .Instance.CurrentLoggedUser)
                .UpdateUserGroup(userGroupId, new UserGroup { Status = newStatus })
                .ToObservable()
                    .SubscribeOn(TaskPoolScheduler.Default)
                    .InterpretingErrors();
        }
    }
}
