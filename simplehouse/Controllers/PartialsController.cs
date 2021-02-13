using simplehouse.DataAccess;
using simplehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace simplehouse.Controllers
{
    public class PartialsController : Controller
    {
        MemberDataAccess memberDA = new MemberDataAccess();
        ContactInfoDataAccess contactInfoDA = new ContactInfoDataAccess();
        FAQDataAccess faqDA = new FAQDataAccess();

        public ActionResult _Menu()
        {
            return PartialView();
        }

        public ActionResult _OurTeam()
        {
            List<MEMBER> members = memberDA.GetAll(true);
            return PartialView(members);
        }
        public ActionResult _ContactForm()
        {
            return PartialView();
        }

        public ActionResult _ContactInfo()
        {
            CONTACTINFO contactInfo = contactInfoDA.Get();
            return PartialView(contactInfo);
        }

        public ActionResult _Faqs()
        {
            List<FAQ> faqs = faqDA.GetAll(true);
            return PartialView(faqs);
        }
    }
}