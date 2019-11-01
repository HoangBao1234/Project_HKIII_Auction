using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_HKIII_Auction.Models
{
    public class AdminDal
    {
        Context context = new Context();
        public List<Admin> GetAdmins()
        {
            return context.Admins.ToList();
        }
    }
}