using System;

namespace SGAM.Elfec.Model.Exceptions
{
    /// <summary>
    /// Esta excepción es lanzada cuando ha ocurrido una excepción no controlada en el lado del servidor
    /// </summary>
    public class ServerSideException : Exception
    {
        public ServerSideException(String message) : base(message)
        {
        }
        public ServerSideException() : base("Ocurrió un error en el servidor, porfavor intentelo nuevamente mas tarde")
        {
        }
    }
}
