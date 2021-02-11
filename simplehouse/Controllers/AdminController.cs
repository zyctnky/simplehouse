using simplehouse.DataAccess;
using simplehouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace simplehouse.Controllers
{
    public class AdminController : Controller
    {
        CategoryDataAccess categoryDA = new CategoryDataAccess();
        StateDataAccess stateDA = new StateDataAccess();

        [Route("admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("admin/kategoriler")]
        public ActionResult Categories()
        {
            List<CATEGORY> categories = categoryDA.GetAll(null);
            return View(categories);
        }

        [Route("admin/kategori-ekle")]
        public ActionResult CategoryInsert()
        {
            PopulateStateDropdownList();
            return View();
        }

        [HttpPost]
        public ActionResult InsertCategoryForm(CATEGORY category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryDA.Insert(category);
                    return RedirectToAction("Categories", "Admin");
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    PopulateStateDropdownList(category.STATE_ID);
                    return View("CategoryInsert", category);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                PopulateStateDropdownList(category.STATE_ID);
                return View("CategoryInsert", category);
            }
        }

        [Route("admin/kategori-duzenle/{id}")]
        public ActionResult CategoryUpdate(int id)
        {
            CATEGORY category = categoryDA.GetById(id);
            PopulateStateDropdownList(category.STATE_ID);
            return View(category);
        }

        [HttpPost]
        public ActionResult UpdateCategoryForm(CATEGORY category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    categoryDA.Update(category);
                    return RedirectToAction("Categories", "Admin");
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    PopulateStateDropdownList(category.STATE_ID);
                    return View("CategoryUpdate", category);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                PopulateStateDropdownList(category.STATE_ID);
                return View("CategoryUpdate", category);
            }
        }

        [Route("admin/kategori-sil/{id}")]
        public ActionResult CategoryDelete(int id)
        {
            CATEGORY category = categoryDA.GetById(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult DeleteCategoryForm(int id)
        {
            CATEGORY category = categoryDA.GetById(id);
            try
            {
                if (ModelState.IsValid)
                {
                    categoryDA.Delete(id);
                    return RedirectToAction("Categories", "Admin");
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    return View("CategoryDelete", category);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                return View("CategoryDelete", category);
            }
        }

        [Route("admin/yiyecekler")]
        public ActionResult Foods()
        {
            return View();
        }

        [Route("admin/takim-uyeleri")]
        public ActionResult Members()
        {
            return View();
        }

        [Route("admin/iletisim-bilgileri")]
        public ActionResult ContactInfo()
        {
            return View();
        }

        [Route("admin/iletisim-formlari")]
        public ActionResult ContactForms()
        {
            return View();
        }

        [Route("admin/sifre-degistir")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        private void PopulateStateDropdownList(object selectedState = null)
        {
            var statesQuery = stateDA.GetAll();
            ViewBag.STATE_ID = new SelectList(statesQuery, "ID", "NAME", selectedState);
        }
    }
}