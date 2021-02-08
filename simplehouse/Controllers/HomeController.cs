using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace simplehouse.Controllers
{
    public class HomeController : Controller
    {
        [Route("~/")]
        public ActionResult Index()
        {
            ViewBag.home = "active";
            ViewBag.about = "";
            ViewBag.contact = "";
            return View();
        }

        [Route("about")]
        public ActionResult About()
        {
            ViewBag.home = "";
            ViewBag.about = "active";
            ViewBag.contact = "";
            return View();
        }

        [Route("contact")]
        public ActionResult Contact()
        {
            ViewBag.home = "";
            ViewBag.about = "";
            ViewBag.contact = "active";
            return View();
        }
    }
}