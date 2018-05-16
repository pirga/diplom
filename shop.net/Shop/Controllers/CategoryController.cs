using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class CategoryController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View("Error");
        }
        [HttpGet]
        public ActionResult GetById(int id)
        {
            ViewBag.Category = GetCategoryById(id);
            ViewBag.Products = GetProductsCategoryById(id);
            return View("index");
        }
        public Category GetCategoryById(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                var category = db.Category.Find(id);
                return category;
            }
        }
        public IEnumerable<Category> GetAllCategories()
        {
            using (DBEntities db = new DBEntities())
            {
                return db.Category.ToList();
            }
        }
        public IEnumerable<Product> GetProductsCategoryById(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                var id_products = db.Product_category.Where(x => x.Id_category == id).ToList().Select(x => x.Id_product).ToList();
                var products = new List<Product>();
                foreach (var item in id_products)
                {
                    var cat = db.Product.Where(x => x.Id == item).ToList().First();
                    if (cat != null)
                    {
                        products.Add(cat);
                    }
                }
                return products;
            }
        }
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Categories = GetAllCategories();
            return View();
        }
        [HttpPost]
        public string Add(Category category)
        {
            using (DBEntities db = new DBEntities())
            {
                db.Category.Add(category);
                db.SaveChanges();
                return $"category add, id={db.Category.ToList().Last().Id}";
            }
        }
        [HttpPost]
        public string Change(Category category)
        {
            using (DBEntities db = new DBEntities())
            {
                var findCategory = db.Category.Find(category.Id);
                if (findCategory == null)
                {
                    return $"category with id={category.Id} not found";
                }
                db.Category.AddOrUpdate(category);
                db.SaveChanges();
                return "change complete";
            }
        }
        [HttpDelete]
        public string Delete(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                var findProduct = db.Category.Find(id);
                db.Category.Remove(findProduct);
                db.SaveChanges();
                return $"category with id={id}, delete";
            }
        }
    }
}