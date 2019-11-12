using Newtonsoft.Json;
using Project_HKIII_Auction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Data.SqlClient;

namespace Project_HKIII_Auction.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        ProductDal PDal = new ProductDal();
        UserDal UDal = new UserDal();
        CategoryDal CDal = new CategoryDal();
        HistoryDal HDal = new HistoryDal();
        Context context = new Context();
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult CreateProduct()
        {
            ViewBag.Start = DateTime.Now;
            ViewBag.Start = String.Format("{0:dd/MM/yyyy}", ViewBag.Start);
            ViewBag.Cate = new SelectList(CDal.GetCategories(), "CId", "CName");
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            ViewBag.Cate = new SelectList(CDal.GetCategories(), "CId", "CName");
            var username = (Project_HKIII_Auction.Common.UserLogin)Session["userSession"];
            product.UId = username.UId;
            product.Incremenent = 0;
            ViewBag.Start = DateTime.Now;
            ViewBag.Start = String.Format("{0:dd/MM/yyyy}", ViewBag.Start);
            if (ModelState.IsValid)
            {
                if (PDal.Create(product))
                {
                    ModelState.Clear();
                    return RedirectToAction("Home");
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
        public ActionResult Profiles(int UId)
        {
            User user = UDal.GetUser(UId); 
            return View(user);
        }
        [HttpPost]
        public ActionResult Profiles(User user)
        {
          
            if (ModelState.IsValid)
            {
                UDal.Update(user);
                return RedirectToAction("Profiles", user);
            }
            else
            {
                return View();
            }
        }
        public ActionResult GetProduct(int PId)
        {
            Product product = PDal.GetProduct(PId);
            return View(product);
        }
        public ActionResult HistoryAuction()
        {
            return View();
        }
        public ActionResult Auction(int PId)
        {
            Product product = PDal.GetProduct(PId);
            return View(product);
        }
        [HttpPost]
        public ActionResult Auction(int PId, int Price)
        {
            var user = (Project_HKIII_Auction.Common.UserLogin)Session["userSession"];
            string sCon = ConfigurationManager.ConnectionStrings["hihi"].ConnectionString;
            SqlConnection conn = new SqlConnection(sCon);

            string query = "Update Products Set Incremenent = "+Price+" Where PId = "+PId;

          string query = "Update Products Set Incremenent = @Price Where PId = @PId";
            string queryHis = "Insert into HistoryAuctions Values(@PId, @UId, @Price, @Status)";

            conn.Open();
            //Update Price Auction
            SqlCommand command = new SqlCommand(query, conn);
            command.Parameters.Add(new SqlParameter("Price", Price));
            command.Parameters.Add(new SqlParameter("PId", PId));
            //Add History Auction
            SqlCommand command1 = new SqlCommand(queryHis, conn);
            command1.Parameters.Add(new SqlParameter("PId", PId));
            command1.Parameters.Add(new SqlParameter("UId", user.UId));
            command1.Parameters.Add(new SqlParameter("Price", Price));
            command1.Parameters.Add(new SqlParameter("Status", "Waiting"));
            command1.ExecuteNonQuery();
            
            int i = command.ExecuteNonQuery();
            conn.Close();
            if (i > 0)
            {
                return RedirectToAction("Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult MyProduct(string UName)
        {
            var P = PDal.GetProducts();
            var C = CDal.GetCategories();
            var U = UDal.GetUsers();

            var list = from u in U join p in P on u.UId equals p.UId join c in C on p.CId equals c.CId select new { u.UName, p.PName, p.Image, p.MinimumPrice, c.CName, p.DateStart, p.DateEnd, p.PId, p.Incremenent, p.Status };
            var MyList = list.Where(e => e.UName.Equals(UName));

            var MyProduct = (object)JsonConvert.SerializeObject(MyList);

            return View(MyProduct);
        }
    }
}