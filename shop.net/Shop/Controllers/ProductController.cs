using Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
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
            ViewBag.Images = GetImagesById(id);
            ViewBag.Categories = GetCategoriesProductById(id);
            return View();
        }

        public IEnumerable<Product_image> GetImagesById(int id)
        {
            using (DBEntities db = new DBEntities())
            {
                var imagesProduct = db.Product_image.Where(x => x.Id_product == id).ToList();
                return imagesProduct;
            }
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
        public List<string> SaveFiles(IEnumerable<HttpPostedFileBase> files)
        {
            var filesList = files.ToList();
            var filesName = new List<string>();
            if (filesList.Count > 0)
            {
                for (int i = 0; i < filesList.Count; i++)
                {
                    var newFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(filesList[i].FileName);
                    var imagePath = @"Images\" + newFileName;
                    Request.Files[i].SaveAs(AppDomain.CurrentDomain.BaseDirectory+ imagePath);
                    filesName.Add(@"..\..\"+imagePath);
                }
            }
            return filesName;
        }
        public void AddImages(int idProduct,IEnumerable<string> imagesPath)
        {
            using (DBEntities db = new DBEntities())
            {
                db.Product_image.AddRange(imagesPath.Select(x=>new Product_image {Id =0, Id_product = idProduct, Photo=x }));
                db.SaveChanges();
            }
        }
        [HttpPost]
        public ActionResult Add(Product product, IEnumerable<HttpPostedFileBase> photoPreview , IEnumerable<HttpPostedFileBase> files)
        {
            using (DBEntities db = new DBEntities())
            {
                if (photoPreview.Count() > 0 & photoPreview.ToList()[0] != null)
                {
                    var filesName = SaveFiles(photoPreview);
                    product.Photo = filesName[0];
                }
                product.Date_add = DateTime.Now;
                db.Product.Add(product);
                db.SaveChanges();
                if (files.Count() > 0 & files.ToList()[0]!=null)
                {
                    var filesName = SaveFiles(files);
                    AddImages(db.Product.ToList().Last().Id, filesName);
                }
                return Index(db.Product.ToList().Last().Id);
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