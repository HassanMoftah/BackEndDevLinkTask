using dbsqlbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndLinkDevTask.Models
{
    public class MDDiscountRule : TBModelBase
    {
        [MDTBProperty(IsdbReadWrite = true)]
        public int ItemCount { get; set; }
        public MDDiscountRule() { }


        public static List<MDDiscountRule> GetAll()
        {
            List<MDDiscountRule> rules = new MDDiscountRule().GetAll<MDDiscountRule>("Id", out string msg);
            return rules;
        }

        public static MDDiscountRule GetById(int id)
        {
            MDDiscountRule rule = new MDDiscountRule().GetByParameter<MDDiscountRule>("Id", id.ToString(), out string msg).FirstOrDefault();
            return rule;
        }
        public override string GetTableName()
        {
            return "TBDiscountRules";
        }
    }
}