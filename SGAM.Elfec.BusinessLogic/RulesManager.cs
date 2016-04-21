using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Interfaces;
using SGAM.Elfec.Security;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;

namespace SGAM.Elfec.BusinessLogic
{
    /// <summary>
    /// Maneja la lógica de negocio de las reglas de directivas de usuario
    /// </summary>
    public static class RulesManager
    {
        /// <summary>
        /// Agrega entidades (usuarios o grupos) a la regla con el id especificado,
        /// Si la lista de entidades está vacia retorna un observable que retorna inmediatamente
        /// </summary>
        /// <param name="userGroupId"></param>
        /// <param name="members"></param>
        /// <param name="callback"></param>
        public static IObservable<Unit> AddEntities(string ruleId, IList<IEntity> entities)
        {
            if (entities.IsEmpty())
                return Observable.Empty<Unit>();
            User user = SessionManager.Instance.CurrentLoggedUser;
            return RestEndpointFactory
                    .Create<IRulesEndpoint>(user.Username, user.AuthenticationToken)
                    .AddEntities(ruleId, entities.IsEmpty() ? "0" :
                    entities.ToString(e => e.Id)).ToObservable();
        }
    }
}
