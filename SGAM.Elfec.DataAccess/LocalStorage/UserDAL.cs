using KitaroDB;
using SGAM.Elfec.Model;

namespace SGAM.Elfec.WebServices.LocalStorage
{
    public class UserDAL
    {
        private const string USER_FILE = "User.sgam";

        public static void SaveUser(User user)
        {
            using (DB database = DB.Create(USER_FILE))
            {
                database.Update(user.Username, user.AuthenticationToken);
            }
        }
    }
}
