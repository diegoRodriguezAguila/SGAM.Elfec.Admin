using SGAM.Elfec.Model;
using System;
using System.Security.Cryptography;

namespace SGAM.Elfec.Security
{
    /// <summary>
    /// Se encarga de proteger el token de autenticación para su guardado en memoria física
    /// </summary>
    public class AuthTokenProtect
    {
        // Create byte array for additional entropy when using Protect method.
        public static byte[] _aditionalEntropy = { 9, 8, 7, 6, 5, 22, 53, 1, 94 };

        /// <summary>
        /// Proteje el token del usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static User ProtectToken(User user)
        {
            if (user == null)
                return null;
            user.AuthenticationToken = ProtectToken(user.AuthenticationToken);
            return user;
        }

        /// <summary>
        /// Desproteje el token del usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static User UnprotectToken(User user)
        {
            if (user == null)
                return null;
            user.AuthenticationToken = UnprotectToken(user.AuthenticationToken);
            return user;
        }

        /// <summary>
        /// Cifra el token según el contexto actual
        /// </summary>
        /// <param name="authToken"></param>
        /// <returns>token cifrado</returns>
        private static String ProtectToken(String authToken)
        {

            try
            {
                return Convert.ToBase64String(
                    ProtectedData.Protect(Convert.FromBase64String(authToken),
                    _aditionalEntropy, DataProtectionScope.CurrentUser));
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

        /// <summary>
        /// Descifra el token según el contexton actual
        /// </summary>
        /// <param name="protectedAuthToken"></param>
        /// <returns>token descifrado</returns>
        private static String UnprotectToken(String protectedAuthToken)
        {
            try
            {
                return Convert.ToBase64String(
                    ProtectedData.Unprotect(Convert.FromBase64String(protectedAuthToken),
                    _aditionalEntropy, DataProtectionScope.CurrentUser));
            }
            catch (CryptographicException)
            {
                return null;
            }
        }

    }
}
