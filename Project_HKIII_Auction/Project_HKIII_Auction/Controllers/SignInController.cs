using Project_HKIII_Auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_HKIII_Auction.Controllers
{
    public class SignInController : Controller
    {
        // GET: SignIn
        UserDal dal = new UserDal();
        public ActionResult Index_Signin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Check_Signin(User user)
        {
            if (dal.Create(user))
            {
                return RedirectToAction("Index", "User");
            }
            else
            {
                ViewBag.Msg = "This user name has already existed. Please recreate another user name";
                return View("Index_Signin");
            }
        }
    }
}