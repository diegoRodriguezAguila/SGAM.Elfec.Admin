using KitaroDB;
using SGAM.Elfec.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAM.Elfec.WebServices.LocalStorage
{
    public class UserDAL
    {
        private const string USER_FILE = "user.sgam";

        public static void SaveUser(User user)
        {
            DB database = DB.Create(USER_FILE);
            database.Insert(user.Username, user.AuthenticationToken);
            database.Dispose();

        }
    }
}
