using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices.JsonContractResolver
{
    public class SnakeCasePropertyNamesContractResolver : DeliminatorSeparatedPropertyNamesContractResolver
    {
        public SnakeCasePropertyNamesContractResolver() : base('_') { }
    }
}
