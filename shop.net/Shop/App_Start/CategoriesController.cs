using Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.App_Start
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        public ActionResult Index()
        {
            ViewBag.Categories = GetAllCategories();
            return View();
        }
        public IEnumerable<Category> GetAllCategories()
        {
            using (DBEntities db = new DBEntities())
            {
                return db.Category.ToList();
            }
        }
    }
}