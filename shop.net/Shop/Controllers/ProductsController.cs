using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ProductsController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var products = GetAll();
            ViewBag.Products = products;
            return View();
        }
        [HttpGet]
        public ActionResult Page(int pageNumber, int pageSize = 10)
        {
            var products = GetAll(pageNumber,pageSize);
            ViewBag.Products = products;
            return View();
        }
        public IEnumerable<Product> GetAll()
        {
            using (DBEntities db = new DBEntities())
            {
                var products = GetAll(0, GetCount());
                return products;
            }
        }
        public IEnumerable<Product> GetAll(int pageNumber,int pageSize)
        {
            using (DBEntities db = new DBEntities())
            {
                var products = db.Product.ToList().Skip(pageNumber * pageSize ).Take(pageSize).ToList();
                return products;
            }
        }
        [HttpGet]
        public int GetCount()
        {
            using (DBEntities db = new DBEntities())
            {
                var count = db.Product.ToList().Count();
                return count;
            }
        }
        [HttpDelete]
        public string DeleteAll()
        {
            using (DBEntities db = new DBEntities())
            {
                db.Product.RemoveRange(db.Product);
                db.SaveChanges();
                return $"delete all products, complete";
            }
        }
    }
}