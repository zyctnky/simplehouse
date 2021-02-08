using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace simplehouse.Controllers
{
    public class PartialsController : Controller
    {
        public ActionResult _Menu()
        {
            return PartialView();
        }

        public ActionResult _OurTeam()
        {
            return PartialView();
        }
        public ActionResult _ContactForm()
        {
            return PartialView();
        }
        public ActionResult _ContactInfo()
        {
            return PartialView();
        }

        public ActionResult _Faqs()
        {
            return PartialView();
        }
    }
}