
using System;
using System.Collections.Generic;
using System.IO;
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
        public bool Create(Product product)
        {
            string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string extenstion = Path.GetExtension(product.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extenstion;//up fole khác ngày thàng ko trùng
            product.Image = "/Image/image_Product/" + fileName;
            fileName = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("/Image/image_Product/"), fileName);
            product.ImageFile.SaveAs(fileName);
            context.Products.Add(product);
            return context.SaveChanges() > 0;
        }

    }
}