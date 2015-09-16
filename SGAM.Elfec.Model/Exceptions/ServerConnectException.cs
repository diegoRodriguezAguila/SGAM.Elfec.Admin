using System;

namespace SGAM.Elfec.Model.Exceptions
{
    public class ServerConnectException : Exception
    {
        private String _extraInfo;

        public ServerConnectException()
        {

        }

        public ServerConnectException(String extraInfo)
        {
            this._extraInfo = extraInfo;
        }

        public override String Message
        {
            get
            {
                return "No se pudo establecer conexión con el servidor, asegurese de que está conectado a internet!" +
                        (_extraInfo != null ? ("\n<i>" + _extraInfo + "</i>") : "");
            }
        }
    }
}
