using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SGAM.Elfec.Model
{
    public class MobileApplication
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public Version Version { get; set; }
        public string PackageName { get; set; }
        public Uri StorageURL { get; set; }
        public Uri ImagePath { get; set; }
        public string Author { get; set; }
        public int Status { get; set; }

        public List<Exception> Validate()
        {
            return null;
        }

    }
}
