using Project_HKIII_Auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_HKIII_Auction.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        UserDal dal = new UserDal();
        public ActionResult Index_Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Check_Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                ViewBag.u = "please enter username";
                ViewBag.U = username;
                return View("Index_Login");
            }
            if (string.IsNullOrEmpty(password))
            {
                ViewBag.p = "please enter Password";
                return View("Index_Login");
            }
            else
            {
                User check = dal.GetUsers().SingleOrDefault(e => e.UName == username);
                if (check != null)
                {
                    if (check.Password.Equals(password))
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ViewBag.MsgP = "Incorrect Password";
                        ViewBag.UserName = username;
                        return View("Index_Login");
                    }
                }
                else
                {
                    ViewBag.MsgU = "Incorrect User Name";

                    return View("Index_Login");
                }
            }
            

        }

    }
}