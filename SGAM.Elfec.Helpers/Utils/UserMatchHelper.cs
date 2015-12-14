using SGAM.Elfec.Model;
using System.Linq;

namespace SGAM.Elfec.Helpers.Utils
{
    public class UserMatchHelper
    {
        /// <summary>
        /// Verifica que los parámetros de busqueda de un usuario coincidan con ese para
        /// que sea agregado o no a un filtro
        /// </summary>
        /// <param name="user"></param>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public static bool MatchesSearchQuery(User user, string searchQuery)
        {
            bool resp = user.FirstName.ToLower().Contains(searchQuery) ||
                        user.LastName.ToLower().Contains(searchQuery) ||
                        user.Username.ToLower().Contains(searchQuery);
            if (!resp && searchQuery.Contains(' ')) // call recursive if not contained
            {
                var searchParams = searchQuery.Split(' ');
                foreach (var searchParam in searchParams)
                {
                    resp = MatchesSearchQuery(user, searchParam);
                    if (resp)
                        return resp;
                }
            }
            return resp;
        }
    }
}
