using SGAM.Elfec.DataAccess.WebServices;
using SGAM.Elfec.DataAccess.WebServices.ApiEndpoints;
using SGAM.Elfec.Helpers.Utils;
using SGAM.Elfec.Model;
using SGAM.Elfec.Model.Interfaces;
using SGAM.Elfec.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Concurrency;
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
        /// Updates a rule information to the server
        /// </summary>
        /// <param name="ruleId"></param>
        /// <param name="rule"></param>
        /// <returns></returns>
        public static IObservable<Rule> Update(Rule rule)
        {
            if (rule.Id == null) throw new InvalidDataException("The provided rule must have an Id");
            return RestEndpointFactory
                .Create<IRulesEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                .Update(rule.Id, rule).ToObservable()
                .SubscribeOn(ThreadPoolScheduler.Instance)
                .InterpretingErrors();
        }

        /// <summary>
        /// Agrega entidades (usuarios o grupos) a la regla con el id especificado
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="entities"></param>
        public static IObservable<Rule> AddEntities(Rule rule, IList<IEntity> entities)
        {
            if (rule.Id == null) throw new InvalidDataException("The provided rule must have an Id");
            if (entities == null || entities.IsEmpty())
                return Observable.Defer(() => Observable.Return(rule));
            return RestEndpointFactory
                .Create<IRulesEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                .AddEntities(rule.Id, entities.IsEmpty()
                    ? "0"
                    : entities.ToString(e => e.Id)).ToObservable()
                .SubscribeOn(ThreadPoolScheduler.Instance)
                .InterpretingErrors();
        }

        /// <summary>
        /// Elimina entidades (usuarios o grupos) a la regla con el id especificado
        /// </summary>
        /// <param name="rule"></param>
        /// <param name="entities"></param>
        public static IObservable<Rule> DeleteEntities(Rule rule, IList<IEntity> entities)
        {
            if (rule.Id == null) throw new InvalidDataException("The provided rule must have an Id");
            if (entities == null || entities.IsEmpty())
                return Observable.Defer(() => Observable.Return(rule));
            return RestEndpointFactory
                .Create<IRulesEndpoint>(SessionManager.Instance.CurrentLoggedUser)
                .DeleteEntities(rule.Id, entities.IsEmpty()
                    ? "0"
                    : entities.ToString(e => e.Id)).ToObservable()
                .SubscribeOn(ThreadPoolScheduler.Instance)
                .InterpretingErrors();
        }
    }
}