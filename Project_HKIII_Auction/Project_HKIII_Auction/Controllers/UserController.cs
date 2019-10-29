using Newtonsoft.Json;
using Project_HKIII_Auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_HKIII_Auction.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        AdminDal Adal = new AdminDal();
        ProductDal PDal = new ProductDal();
        UserDal UDal = new UserDal();
        CategoryDal CDal = new CategoryDal();
        HistoryDal HDal = new HistoryDal();
        Context context = new Context();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Tivi()
        {
            return View();
        }
        public ActionResult CreateProduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                if (PDal.Create(product))
                {
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult Home()
        {

            var U = UDal.GetUsers();
            var P = PDal.GetProducts();
            var C = CDal.GetCategories();
            var list = from u in U join p in P on u.UId equals p.UId join c in C on p.CId equals c.CId select new {u.UName, p.PName, p.Image, p.MinimumPrice, c.CName, p.DateStart, p.DateEnd, p.PId, p.Incremenent, p.Status };
            var convert = (object)JsonConvert.SerializeObject(list);
            return View(convert);
        }
    }
}