using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbsqlbase
{

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class MDTBProperty: Attribute
    {
        public bool IsdbReadWrite { get; set; } = true;
    }

    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class JsonProperty : Attribute
    {
        public bool IsdbReadWrite { get; set; } = true;
    }

}
