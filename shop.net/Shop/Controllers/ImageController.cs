using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ImageController : Controller
    {
        // GET: Image
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadImage()
        {
            return View();
        }
        public ActionResult test()
        {
            return View();
        }
            
    }
}