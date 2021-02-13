using simplehouse.DataAccess;
using simplehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace simplehouse.Controllers
{
    public class HomeController : Controller
    {
        ContactFormDataAccess contactFormDA = new ContactFormDataAccess();

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


        [HttpPost]
        public ActionResult ContactForm(CONTACTFORM contactForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    contactFormDA.Insert(contactForm);
                    return Json(new { SuccessMsg = "Mesajınız iletildi." });
                }
                catch (Exception ex)
                {
                    return Json(new { ErrorMsg = "Try Again." });
                }
            }
            else
                return Json(new { ErrorMsg = "Check All Fields." });
        }
    }
}