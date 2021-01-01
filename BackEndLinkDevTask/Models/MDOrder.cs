using BackEndLinkDevTask.Buisness;
using dbsqlbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndLinkDevTask.Models
{
    public class MDOrder : TBModelBase
    {
        [MDTBProperty(IsdbReadWrite = true)]
        public int ProductId { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public int UserId { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public float TotalPrice { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public int Quantity { get; set; }
        public MDOrder() { }

        public static List<MDOrder> GetAll()
        {
            List<MDOrder> orders = new MDOrder().GetAll<MDOrder>("Id", out string msg);
            return orders;
        }
        public static MDOrder Add(MDOrder order)
        {
            order.TotalPrice = CalcTotalPrice(order);
            MDOrder addedorder = order.Add<MDOrder>(out string msg);
            return addedorder;
        }
        public static float CalcTotalPrice(MDOrder order)
        {
            MDProduct product = MDProduct.GetById(order.ProductId);
            ProductVsDiscountRuleFinalPrice discountRuleFinalPrice=new ProductVsDiscountRuleFinalPrice(new List<MDProduct>() { product});
            product = discountRuleFinalPrice.CalcFinalPriceAfterEveryDiscountRule()[0];
            if(product.ProductVsDiscountRules==null||product.ProductVsDiscountRules.Count==0)
            {
                return (order.Quantity * product.Price);
            }
            else
            {
                MDProductVsDiscountRule ruleoneItem = product.ProductVsDiscountRules.Where(x => x.DiscountRule.ItemCount == 1).SingleOrDefault();
                MDProductVsDiscountRule ruletwoItem = product.ProductVsDiscountRules.Where(x => x.DiscountRule.ItemCount == 1).SingleOrDefault();
                int quantity = order.Quantity;
                float sum = 0;
                while (quantity>0)
                {
                    if(ruletwoItem!=null&&quantity>=2)
                    {
                        sum += ruletwoItem.FinalPrice;
                        quantity = quantity - 2;
                    }
                    else if(ruleoneItem!=null&&quantity>=1)
                    {
                        sum += ruleoneItem.FinalPrice;
                        quantity = quantity - 1;
                    }
                    else
                    {
                        sum += quantity * product.Price;
                        break;
                    }
                }
                return sum;
            }
        }

        public override string GetTableName()
        {
            return "TBOredrs";
        }
    }
}