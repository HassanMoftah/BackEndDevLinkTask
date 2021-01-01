using dbsqlbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndLinkDevTask.Models
{
    public class MDProductVsDiscountRule : TBModelBase
    {
        [MDTBProperty(IsdbReadWrite = true)]
        public int ProductId { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public int DiscountRuleId { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public int Percentage { get; set; }
        public float  FinalPrice { get; set; }
        public MDDiscountRule DiscountRule { get; set; }
        public MDProductVsDiscountRule() { }

        public static List<MDProductVsDiscountRule> GetDiscountsOfProduct(int productid)
        {
            List<MDProductVsDiscountRule> rules = new MDProductVsDiscountRule().GetByParameter<MDProductVsDiscountRule>("ProductId", productid.ToString(), out string msg);
            for(int i=0;i<rules.Count;i++)
            {
                rules[i].DiscountRule = MDDiscountRule.GetById(rules[i].DiscountRuleId);
            }
            return rules;
        }

        public static MDProductVsDiscountRule Add(MDProductVsDiscountRule ProductVsDiscount)
        {
            DeleteIfExist(ProductVsDiscount);
            MDProductVsDiscountRule productVsDiscountRuleAdded = ProductVsDiscount.Add<MDProductVsDiscountRule>(out string msg);
            return ProductVsDiscount;
        }
        public static bool Delete(MDProductVsDiscountRule ProductVsDiscount)
        {
            bool deleted = ProductVsDiscount.Delete();
            return deleted;
        }
        private static void DeleteIfExist(MDProductVsDiscountRule ProductVsDiscount)
        {
            string query = $"select  * from TBProductVsDiscountRule where productId={ProductVsDiscount.ProductId} and DiscountRuleId={ProductVsDiscount.DiscountRuleId}";
            MDProductVsDiscountRule checkProductVsDiscount = new MDProductVsDiscountRule().GetByQuery<MDProductVsDiscountRule>(query, out string msg).FirstOrDefault();
            if (checkProductVsDiscount!=null)
            {
                Delete(checkProductVsDiscount);
            }
        }
        public override string GetTableName()
        {
            return "TBProductVsDiscountRule";
        }
    }
}