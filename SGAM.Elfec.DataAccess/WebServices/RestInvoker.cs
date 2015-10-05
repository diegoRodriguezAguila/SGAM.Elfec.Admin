﻿using RestEase;
using SGAM.Elfec.Model.Callbacks;
using SGAM.Elfec.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices
{
    /// <summary>
    /// Representa la llamada a un web service
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public delegate Task<T> WebServiceAsyncCallDelegate<T>();
    /// <summary>
    /// Representa la llamada despues del webservice pasandole el resultado del mismo, se ejecuta antes de
    /// llamar a OnSuccess 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    public delegate void PostCallHandler<T>(T result);

    /// <summary>
    /// Invoca servicios REST
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RestInvoker<T>
    {
        /// <summary>
        /// Evento que se ejecuta despues de la llamada al webservice
        /// y antes de la llamada de OnSuccess del Callback
        /// </summary>
        public event PostCallHandler<T> PostCall;

        /// <summary>
        /// Realiza la llamada al webservice con sus callback y la manipulación de errores necesaria
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="wsAsyncCall"></param>
        public async void InvokeWebService(ResultCallback<T> callback,
            WebServiceAsyncCallDelegate<T> wsAsyncCall)
        {
            IList<Exception> errors = new List<Exception>();
            try
            {
                T wsResult = await wsAsyncCall();
                if (PostCall != null)
                    PostCall(wsResult);
                callback.OnSuccess(null, wsResult);
            }
            catch (ApiException e)
            {
                errors.Add(RestErrorInterpreter.InterpretError(e));
            }
            catch (HttpRequestException)
            {
                errors.Add(new ServerConnectException());
            }
           catch (Exception e)
            {
                errors.Add(e);
            }
            if (errors.Count > 0)
                callback.OnFailure(null, errors.ToArray());
        }
    }
}