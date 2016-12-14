using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Helpers.Text;
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
    public class PolicyManager
    {
        /// <summary>
        /// Obtiene todas las directivas de usuario, ya sea por medio de la caché o de webservices
        /// </summary>
        /// <param name="callback"></param>
        public static IObservable<IList<Policy>> GetAllPolicies()
        {
            var parameters = new Dictionary<string, string>();
            parameters["sort"] = "-status,type";
            parameters["include"] = "rules.entities";
            return RestEndpointFactory
                    .Create<IPoliciesEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                    .GetAllPolicies(parameters)
                    .ToObservable()
                    .SubscribeOn(NewThreadScheduler.Default)
                    .InterpretingErrors();
        }

        /// <summary>
        /// Registra una regla en la politica con el id proporcionado
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static IObservable<Rule> RegisterRule(PolicyType policyId, Rule rule)
        {
            return RestEndpointFactory
                .Create<IPoliciesEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                    .RegisterRule(policyId.ToString().FromCamelToSnakeCase(), rule,
                    rule.Entities.ToString(e => e.Id)).ToObservable()
                    .SubscribeOn(NewThreadScheduler.Default)
                    .InterpretingErrors();
        }

        /// <summary>
        /// Registra una regla en la politica con el id proporcionado
        /// </summary>
        /// <param name="policyId"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        public static IObservable<Unit> DeleteRules(PolicyType policyId, params Rule[] rules)
        {
            if (rules.IsEmpty())
                return Observable.Defer(() => Observable.Return(Unit.Default));
            return RestEndpointFactory.Create<IPoliciesEndpoint>(SessionManager
                .Instance.CurrentLoggedUser)
                    .DeleteRules(policyId.ToString().FromCamelToSnakeCase(),
                    rules.ToString(r => r.Id))
                    .ToObservable()
                    .SubscribeOn(NewThreadScheduler.Default)
                    .InterpretingErrors();
        }

    }
}
