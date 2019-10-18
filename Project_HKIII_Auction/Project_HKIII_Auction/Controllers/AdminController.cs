using Newtonsoft.Json;
using Project_HKIII_Auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_HKIII_Auction.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        AdminDal Adal = new AdminDal();
        ProductDal PDal = new ProductDal();
        UserDal UDal = new UserDal();
        CategoryDal CDal = new CategoryDal();
        public ActionResult Index()
        {
            ViewBag.ACount = Adal.GetAdmins().Count();
            ViewBag.PCount = PDal.GetProducts().Count();
            ViewBag.UCount = UDal.GetUsers().Count();

            var Cates = CDal.GetCategories();
            var Pros = PDal.GetProducts();
            var Users = UDal.GetUsers();

            var List = from C in Cates join P in Pros on C.CId equals P.CId join U in Users on P.UId equals U.UId select new {U.UName, P.PName, C.CName, P.MinimumPrice, P.DateStart, P.DateEnd };
            var PList = (object)JsonConvert.SerializeObject(List);
            return View(PList);
        }
    }
}