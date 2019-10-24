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
        public bool Create(User user)
        {
            User check = context.Users.SingleOrDefault(e=>e.UName == user.UName);
            if (check == null)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}