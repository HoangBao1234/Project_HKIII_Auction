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
        HistoryDal HDal = new HistoryDal();
        Context context = new Context();
        public ActionResult Index()
        {
            ViewBag.CCount = CDal.GetCategories().Count();
            ViewBag.PCount = PDal.GetProducts().Count();
            ViewBag.UCount = UDal.GetUsers().Count();

            var Cates = CDal.GetCategories();
            var Pros = PDal.GetProducts();
            var Users = UDal.GetUsers();

            var List = from C in Cates join P in Pros on C.CId equals P.CId join U in Users on P.UId equals U.UId select new {U.UName, P.PName, C.CName, P.MinimumPrice, P.DateStart, P.DateEnd };
            var PList = (object)JsonConvert.SerializeObject(List);
            return View(PList);
        }
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                if (CDal.Create(category))
                {
                    return RedirectToAction("Categories");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View(category);
            }
        }
        public ActionResult Categories()
        {
            return View(CDal.GetCategories());
        }
        public ActionResult Users()
        {
            return View(UDal.GetUsers());
        }
        public ActionResult Products()
        {
            return View(PDal.GetProducts());
        }
        [HttpPost]
        public ActionResult SreachUs(string txtSearch)
        {
            if (string.IsNullOrEmpty(txtSearch))
            {
                return View("Users", UDal.GetUsers()); ;
            }
            else
            {
                var list = UDal.GetUsers().Where(e => e.UName.ToLower().Contains(txtSearch) || e.UName.ToUpper().Contains(txtSearch));
                return View("Users", list.ToList());
            }
        }
        [HttpPost]
        public ActionResult SreachPro(string txtSearch)
        {
            if (string.IsNullOrEmpty(txtSearch))
            {
                return View("Products", PDal.GetProducts());
            }
            else
            {
                var list = PDal.GetProducts().Where(e=>e.PName.ToLower().Contains(txtSearch) || e.PName.ToUpper().Contains(txtSearch));
                return View("Products", list.ToList());
            }
        }
        [HttpPost]
        public ActionResult SearchCate(string txtSearch)
        {
            if (string.IsNullOrEmpty(txtSearch))
            {
                return View("categories", CDal.GetCategories());
            }
            else
            {
                var list = CDal.GetCategories().Where(e => e.CName.ToUpper().Contains(txtSearch) || e.CName.ToLower().Contains(txtSearch));
                return View("Categories", list.ToList());
            }
        }

        public ActionResult HistoryAuction()
        {
            var His = HDal.GetHistoryAuctions();
            var Pros = PDal.GetProducts();
            var Users = UDal.GetUsers();

            var List = from U in Users join H in His on U.UId equals H.UId join P in Pros on H.PId equals P.PId select new {U.UName, P.PName, P.MinimumPrice, H.PriceAuction, H.Status };
            var model = (object)JsonConvert.SerializeObject(List);
            return View(model);
        }
        [HttpPost]
        public ActionResult SearchHist(string txtSearch)
        {
            var His = HDal.GetHistoryAuctions();
            var Pros = PDal.GetProducts();
            var Users = UDal.GetUsers();

            var List = from U in Users join H in His on U.UId equals H.UId join P in Pros on H.PId equals P.PId select new { U.UName, P.PName, P.MinimumPrice, H.PriceAuction, H.Status };
            var model = (object)JsonConvert.SerializeObject(List);
            if (string.IsNullOrEmpty(txtSearch))
            {
                return View("HistoryAuction", model);
            }
            else
            {
                var searched = List.Where(e=>e.UName.ToLower().Contains(txtSearch) || e.UName.ToUpper().Contains(txtSearch));
                var model1 = (object)JsonConvert.SerializeObject(searched);
                return View("HistoryAuction", model1);
            }
        }
    }
}