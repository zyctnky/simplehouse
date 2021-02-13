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
        FoodDataAccess foodDA = new FoodDataAccess();
        CategoryDataAccess categoryDA = new CategoryDataAccess();

        public ActionResult _Menu()
        {
            List<CATEGORY> categories = categoryDA.GetAll(true);
            return PartialView(categories);
        }

        public ActionResult _MenuDetails(int id)
        {
            List<FOOD> foods = foodDA.GetByCategoryId(id, true);
            return PartialView(foods);
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