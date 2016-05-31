using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Interfaces;
using SGAM.Elfec.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace SGAM.Elfec.BusinessLogic
{
    public static class EntitiesManager
    {
        /// <summary>
        /// Gets the observable for subscription to the call to webservices
        /// </summary>
        /// <returns></returns>
        public static IObservable<IList<IEntity>> GetAllEntities()
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            var parameters = new Dictionary<string, string>();
            parameters["sort"] = "username,name";
            parameters["status"] = "1";
            var getUsersObs = RestEndpointFactory
                    .Create<IUsersEndpoint>(user)
                    .GetAllUsers(parameters).ToObservable();
            var getUserGroupsObs = RestEndpointFactory
                    .Create<IUserGroupsEndpoint>(user)
                    .GetAllUserGroups(parameters).ToObservable();
            return Observable.Zip(getUsersObs, getUserGroupsObs, (users, userGroups) =>
             {
                 List<IEntity> entities = new List<IEntity>();
                 entities.AddRange(users);
                 entities.AddRange(userGroups);
                 return entities.OrderBy(e => e.Name).ToList();
             }).SubscribeOn(NewThreadScheduler.Default)
             .InterpretingErrors();
        }
    }
}
