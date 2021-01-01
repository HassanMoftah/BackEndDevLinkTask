using dbsqlbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndLinkDevTask.Models
{
    public class MDCategory : TBModelBase
    {
        [MDTBProperty(IsdbReadWrite = true)]
        public string Name { get; set; }

        public MDCategory() { }

        public static List<MDCategory> GetAll()
        {
            List<MDCategory> categories = new MDCategory().GetAll<MDCategory>("Id", out string msg);
            return categories;
        }
        public override string GetTableName()
        {
            return "TBCategories";
        }
    }
}