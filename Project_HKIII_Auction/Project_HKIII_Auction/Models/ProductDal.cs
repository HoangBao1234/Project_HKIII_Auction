using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_HKIII_Auction.Models
{
    public class ProductDal
    {
        Context context = new Context();
        public List<Product> GetProducts()
        {
            return context.Products.ToList();
        }
    }
}