using SGAM.Elfec.Model.Callbacks;
using System;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices
{
    /// <summary>
    /// Invoca servicios REST
    /// </summary>
    public class RestInvoker
    {

        /// <summary>
        /// Realiza la llamada al webservice con sus callback y la manipulación de errores necesaria
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="restCall"></param>
        public static async void InvokeWebService<T>(ResultCallback<T> callback, Task<T> restCall)
        {
            try
            {
                T wsResult = await restCall;
                callback.OnSuccess(null, wsResult);
            }
            catch (Exception e)
            {
                CallBackFailure(callback, e);
            }
        }


        /// <summary>
        /// Realiza la llamada al webservice con sus callback y la manipulación de errores necesaria
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="restCall"></param>
        public static async void InvokeWebService(VoidCallback callback, Task restCall)
        {
            try
            {
                await restCall;
                callback.OnSuccess(null);
            }
            catch (Exception e)
            {
                CallBackFailure(callback, e);
            }
        }

        /// <summary>
        /// Llama al failure del callback
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="e"></param>
        private static void CallBackFailure(Callback callback, Exception e)
        {
            callback.AddErrors(RestErrorInterpreter.InterpretWebServiceError(e));
            callback.OnFailure(null);
        }

    }
}
