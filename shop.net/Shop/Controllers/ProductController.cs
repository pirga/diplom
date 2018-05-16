using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [HttpGet]
        public ActionResult Index(int id)
        {
            ViewBag.Product = GetProductById(id);
            ViewBag.Categories = GetCategoriesProductById(id);
            return View();
        }
        public Product GetProductById(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                var product = db.Product.Find(id);
                return product;
            }
        }
        public IEnumerable<Category> GetCategoriesProductById(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                var id_categories = db.Product_category.Where(x => x.Id_product == id).ToList().Select(x=>x.Id_product).ToList();
                var categories = new List<Category>();
                foreach(var item in id_categories)
                {
                    var cat = db.Category.Where(x => x.Id == item).ToList().First();
                    if(cat!=null)
                    {
                        categories.Add(cat);
                    }
                }
                return categories;
            }
        }
        public void GetRateProductById(int id)
        {

        }
        public void GetCommentsProductById(int id)
        {

        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPut]
        public string Add(Product product)
        {
            using (DBEntities db = new DBEntities())
            {
                product.Date_add = DateTime.Now;
                db.Product.Add(product);
                db.SaveChanges();
                return $"add product complete, id={db.Product.ToList().Last().Id}";
            }
        }
        [HttpPost]
        public string Change(Product product)
        {
            using (DBEntities db = new DBEntities())
            {
                var findProduct = db.Product.Find(product.Id);
                if(findProduct==null)
                {
                    return $"product with id={product.Id} not found";
                }
                db.Product.AddOrUpdate(product);
                db.SaveChanges();
                return "change complete";
            }
        }
        [HttpDelete]
        public string Delete(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                var findProduct = db.Product.Find(id);
                db.Product.Remove(findProduct);
                db.SaveChanges();
                return $"product with id={id}, delete";
            }
        }
    }
}