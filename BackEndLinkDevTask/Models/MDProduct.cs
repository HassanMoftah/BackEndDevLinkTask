using BackEndLinkDevTask.Buisness;
using dbsqlbase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndLinkDevTask.Models
{
    public class MDProduct : TBModelBase
    {
        [MDTBProperty(IsdbReadWrite = true)]
        public string Name { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public string Description { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]
        public float Price { get; set; }
        [MDTBProperty(IsdbReadWrite = true)]


        public List<MDProductVsDiscountRule> ProductVsDiscountRules { get; set; }
        public int CategoryId { get; set; }
        public MDProduct() { }
        public static List<MDProduct> GetAll()
        {
            List<MDProduct> products = new MDProduct().GetAll<MDProduct>( "Id", out string msg);
            for (int i = 0; i < products.Count; i++)
            {
                products[i].ProductVsDiscountRules = MDProductVsDiscountRule.GetDiscountsOfProduct(products[i].Id);
            }
            ProductVsDiscountRuleFinalPrice productVsDiscountRuleFinalPrice = new ProductVsDiscountRuleFinalPrice(products);
            products = productVsDiscountRuleFinalPrice.CalcFinalPriceAfterEveryDiscountRule();
            return products;

        }
        public static List<MDProduct> GetProductsPaginatedByOffset(int offset, int rowscount)
        {
            List<MDProduct> products = new MDProduct().GetAll<MDProduct>(offset, rowscount, "Price", out string msg);
            for(int i=0;i<products.Count;i++)
            {
                products[i].ProductVsDiscountRules = MDProductVsDiscountRule.GetDiscountsOfProduct(products[i].Id);
            }
            ProductVsDiscountRuleFinalPrice productVsDiscountRuleFinalPrice = new ProductVsDiscountRuleFinalPrice(products);
            products = productVsDiscountRuleFinalPrice.CalcFinalPriceAfterEveryDiscountRule();
            return products;
        }

        public static MDProduct Add(MDProduct product)
        {
            MDProduct productAdded = product.Add<MDProduct>(out string msg);
            return productAdded;
        }

        public static MDProduct GetById(int id)
        {
            MDProduct product = new MDProduct().GetByParameter<MDProduct>("Id", id.ToString(), out string msg).FirstOrDefault();
            if (product != null)
            {
                product.ProductVsDiscountRules = MDProductVsDiscountRule.GetDiscountsOfProduct(product.Id);
            }
            return product;
        }

        public override string GetTableName()
        {
            return "TBProducts";
        }
    }
}