using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_HKIII_Auction.Models
{
    public class UserDal
    {
        Context context = new Context();
        public List<User> GetUsers()
        {
            return context.Users.ToList();
        }
    }
}