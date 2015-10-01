using NDatabase;
using SGAM.Elfec.Model;

namespace SGAM.Elfec.WebServices.LocalStorage
{
    public class UserDAL
    {
        private const string USER_DB = "User.sgam";

        /// <summary>
        /// Representa al usuario logeado actual, solo puede existir un usuario actual a la vez
        /// </summary>
        public static User CurrentUser
        {
            get
            {
                using (var odb = OdbFactory.Open(USER_DB))
                {
                    return odb.QueryAndExecute<User>().GetFirst();
                }
            }
            set
            {
                using (var odb = OdbFactory.Open(USER_DB))
                {
                    var currentUser = odb.QueryAndExecute<User>().GetFirst();
                    if (currentUser != null)
                        odb.Delete(currentUser);
                    odb.Store(value);
                }
            }
        }
    }
}
