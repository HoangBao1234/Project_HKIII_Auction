using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_HKIII_Auction.Models
{
    public class CategoryDal
    {
        Context context = new Context();
        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }
    }
}