using SGAM.Elfec.Model;
using System.Linq;

namespace SGAM.Elfec.Helpers.Utils
{
    public class DeviceMatchHelper
    {
        /// <summary>
        /// Verifica que los parámetros de busqueda de un dispositivo coincidan con ese para
        /// que sea agregado o no a un filtro
        /// </summary>
        /// <param name="device"></param>
        /// <param name="searchQuery"></param>
        /// <returns></returns>
        public static bool MatchesSearchQuery(Device device, string searchQuery)
        {
            var resp = device.Name.ToLower().Contains(searchQuery) ||
                       device.Imei.ToLower().Contains(searchQuery) ||
                       (device.PhoneNumber != null && device.PhoneNumber.ToLower().Contains(searchQuery)) ||
                       (device.WifiMacAddress != null && device.WifiMacAddress.ToLower().Contains(searchQuery));
            if (resp || !searchQuery.Contains(' ')) return resp;
            var searchParams = searchQuery.Split(' ');
            return searchParams.Any(searchParam => MatchesSearchQuery(device, searchParam));
        }
    }
}