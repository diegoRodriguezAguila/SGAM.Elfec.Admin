using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Helpers.Text;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Callbacks;
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
    public class PolicyManager
    {
        /// <summary>
        /// Obtiene todas las directivas de usuario, ya sea por medio de la caché o de webservices
        /// </summary>
        /// <param name="callback"></param>
        public static void GetAllPolicies(ResultCallback<IList<Policy>> callback)
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            var parameters = new Dictionary<string, string>();
            parameters["sort"] = "-status,type";
            parameters["include"] = "rules.entities";
            RestInvoker.InvokeWebService(callback, RestEndpointFactory
                    .Create<IPoliciesEndpoint>(user.Username, user.AuthenticationToken)
                    .GetAllPolicies(parameters));
        }

        /// <summary>
        /// Registra una regla en la politica con el id proporcionado
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static IObservable<Rule> RegisterRule(PolicyType policyId, Rule rule)
        {
            User user = SessionManager.Instance.CurrentLoggedUser;
            return RestEndpointFactory.Create<IPoliciesEndpoint>(user.Username, user.AuthenticationToken)
                    .RegisterRule(policyId.ToString().FromCamelToSnakeCase(), rule,
                    rule.Entities.ToString(e => e.Id)).ToObservable()
                    .SubscribeOn(NewThreadScheduler.Default)
                    .InterpretingErrors();
        }

        /// <summary>
        /// Registra una regla en la politica con el id proporcionado
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static IObservable<Unit> DeleteRules(PolicyType policyId, params Rule[] rules)
        {
            if (rules.IsEmpty())
                return Observable.Defer(() => Observable.Return(Unit.Default));
            User user = SessionManager.Instance.CurrentLoggedUser;
            return RestEndpointFactory.Create<IPoliciesEndpoint>(user.Username, user.AuthenticationToken)
                    .DeleteRules(policyId.ToString().FromCamelToSnakeCase(),
                    rules.ToString(r => r.Id))
                    .ToObservable()
                    .SubscribeOn(NewThreadScheduler.Default)
                    .InterpretingErrors();
        }

    }
}
