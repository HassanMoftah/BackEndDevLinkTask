using BackEndLinkDevTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndLinkDevTask.Buisness
{
    public class ProductVsDiscountRuleFinalPrice
    {
        private List<MDProduct> products;
        public ProductVsDiscountRuleFinalPrice(List<MDProduct> _products)
        {
            products = _products;
        }
        public List<MDProduct> CalcFinalPriceAfterEveryDiscountRule()
        {
            for(int i=0;i<products.Count;i++)
            {
                for(int j = 0; j < products[i].ProductVsDiscountRules.Count; j++)
                {
                   
                        float price = products[i].Price* products[i].ProductVsDiscountRules[j].DiscountRule.ItemCount;
                        float discount = (products[i].ProductVsDiscountRules[j].Percentage / 100)*price;
                        products[i].ProductVsDiscountRules[j].FinalPrice=price - discount;
                   
                }
            }
            return products;
        }
    }
}