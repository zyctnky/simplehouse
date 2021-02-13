using simplehouse.DataAccess;
using simplehouse.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace simplehouse.Controllers
{
    public class AdminController : Controller
    {
        CategoryDataAccess categoryDA = new CategoryDataAccess();
        StateDataAccess stateDA = new StateDataAccess();
        FoodDataAccess foodDA = new FoodDataAccess();
        MemberDataAccess memberDA = new MemberDataAccess();
        ContactInfoDataAccess contactInfoDA = new ContactInfoDataAccess();
        ContactFormDataAccess contactFormDA = new ContactFormDataAccess();
        UserDataAccess userDA = new UserDataAccess();

        [Route("admin")]
        public ActionResult Index()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Login");

            return View();
        }

        #region Account

        [Route("login")]
        public ActionResult Login()
        {
            if (Session["ID"] != null)
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public ActionResult LoginForm(USER user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (userDA.IsLoginOK(user))
                    {
                        USER _user = userDA.GetByEmail(user.EMAIL);
                        Session["ID"] = _user.ID;
                        Session["EMAIL"] = _user.EMAIL;
                        Session["NAME"] = _user.NAME;

                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        ViewBag.Error = "Kullanıcı adı veya şifre hatalı.";
                        return View("Login", user);
                    }
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    return View("Login", user);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                return View("Login", user);
            }
        }

        [Route("logout")]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Login");
        }

        #endregion

        #region Kategori
        [Route("admin/kategoriler")]
        public ActionResult Categories()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            List<CATEGORY> categories = categoryDA.GetAll(null);
            return View(categories);
        }

        [Route("admin/kategori-ekle")]
        public ActionResult CategoryInsert()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            PopulateStateDropdownList();
            return View();
        }

        [HttpPost]
        public ActionResult InsertCategory(CATEGORY category)
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
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            CATEGORY category = categoryDA.GetById(id);
            PopulateStateDropdownList(category.STATE_ID);
            return View(category);
        }

        [HttpPost]
        public ActionResult UpdateCategory(CATEGORY category)
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
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            CATEGORY category = categoryDA.GetById(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult DeleteCategory(int id)
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
        #endregion

        #region Yiyecekler

        [Route("admin/yiyecekler")]
        public ActionResult Foods()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            List<FOOD> foods = foodDA.GetAll(null);
            return View(foods);
        }

        [Route("admin/yiyecek/{id}")]
        public ActionResult Food(int id)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            FOOD food = foodDA.GetById(id);
            return View(food);
        }

        [Route("admin/yiyecek-ekle")]
        public ActionResult FoodInsert()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            PopulateStateDropdownList();
            PopulateCategoryDropdownList();
            return View();
        }

        [HttpPost]
        public ActionResult InsertFood(FOOD food)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                        string uzanti = Path.GetExtension(Request.Files[0].FileName);
                        string tamYolYeri = "~/Content/images/foods/" + dosyaAdi + uzanti;
                        Request.Files[0].SaveAs(Server.MapPath(tamYolYeri));
                        food.IMAGE = dosyaAdi + uzanti;
                    }
                    else
                    {
                        ViewBag.Error = "Resim eklemelisiniz.";
                        PopulateStateDropdownList(food.STATE_ID);
                        PopulateCategoryDropdownList(food.CATEGORY_ID);
                        return View("FoodInsert", food);
                    }

                    foodDA.Insert(food);
                    return RedirectToAction("Foods", "Admin");
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    PopulateStateDropdownList(food.STATE_ID);
                    PopulateCategoryDropdownList(food.CATEGORY_ID);
                    return View("FoodInsert", food);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                PopulateStateDropdownList(food.STATE_ID);
                PopulateCategoryDropdownList(food.CATEGORY_ID);
                return View("FoodInsert", food);
            }
        }

        [Route("admin/yiyecek-duzenle/{id}")]
        public ActionResult FoodUpdate(int id)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            FOOD food = foodDA.GetById(id);
            PopulateStateDropdownList(food.STATE_ID);
            PopulateCategoryDropdownList(food.CATEGORY_ID);
            return View(food);
        }

        [HttpPost]
        public ActionResult UpdateFood(FOOD food)
        {
            FOOD _food = foodDA.GetById(food.ID);
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/Content/images/foods/" + _food.IMAGE)))
                            System.IO.File.Delete(Server.MapPath("~/Content/images/foods/" + _food.IMAGE));

                        string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                        string uzanti = Path.GetExtension(Request.Files[0].FileName);
                        string tamYolYeri = "~/Content/images/foods/" + dosyaAdi + uzanti;
                        Request.Files[0].SaveAs(Server.MapPath(tamYolYeri));
                        food.IMAGE = dosyaAdi + uzanti;
                    }
                    else
                        food.IMAGE = _food.IMAGE;

                    foodDA.Update(food);
                    return RedirectToAction("Foods", "Admin");
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    PopulateStateDropdownList(_food.STATE_ID);
                    PopulateCategoryDropdownList(_food.CATEGORY_ID);
                    return View("FoodUpdate", _food);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                PopulateStateDropdownList(_food.STATE_ID);
                PopulateCategoryDropdownList(_food.CATEGORY_ID);
                return View("FoodUpdate", _food);
            }
        }

        [Route("admin/yiyecek-sil/{id}")]
        public ActionResult FoodDelete(int id)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            FOOD food = foodDA.GetById(id);
            return View(food);
        }

        [HttpPost]
        public ActionResult DeleteFood(int id)
        {
            FOOD food = foodDA.GetById(id);
            try
            {
                if (ModelState.IsValid)
                {
                    foodDA.Delete(id);

                    if (System.IO.File.Exists(Server.MapPath("~/Content/images/foods/" + food.IMAGE)))
                        System.IO.File.Delete(Server.MapPath("~/Content/images/foods/" + food.IMAGE));

                    return RedirectToAction("Foods", "Admin");
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    return View("FoodDelete", food);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                return View("FoodDelete", food);
            }
        }

        #endregion

        #region Takım Üyeleri

        [Route("admin/takim-uyeleri")]
        public ActionResult Members()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            List<MEMBER> members = memberDA.GetAll(null);
            return View(members);
        }

        [Route("admin/takim-uyesi/{id}")]
        public ActionResult Member(int id)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            MEMBER member = memberDA.GetById(id);
            return View(member);
        }

        [Route("admin/takim-uyesi-ekle")]
        public ActionResult MemberInsert()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            PopulateStateDropdownList();
            return View();
        }

        [HttpPost]
        public ActionResult InsertMember(MEMBER member)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0)
                    {
                        string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                        string uzanti = Path.GetExtension(Request.Files[0].FileName);
                        string tamYolYeri = "~/Content/images/members/" + dosyaAdi + uzanti;
                        Request.Files[0].SaveAs(Server.MapPath(tamYolYeri));
                        member.IMAGE = dosyaAdi + uzanti;
                    }
                    else
                    {
                        ViewBag.Error = "Resim eklemelisiniz.";
                        PopulateStateDropdownList(member.STATE_ID);
                        return View("MemberInsert", member);
                    }

                    memberDA.Insert(member);
                    return RedirectToAction("Members", "Admin");
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    PopulateStateDropdownList(member.STATE_ID);
                    return View("MemberInsert", member);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                PopulateStateDropdownList(member.STATE_ID);
                return View("MemberInsert", member);
            }
        }

        [Route("admin/takim-uyesi-duzenle/{id}")]
        public ActionResult MemberUpdate(int id)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            MEMBER member = memberDA.GetById(id);
            PopulateStateDropdownList(member.STATE_ID);
            return View(member);
        }

        [HttpPost]
        public ActionResult UpdateMember(MEMBER member)
        {
            MEMBER _member = memberDA.GetById(member.ID);
            try
            {
                if (ModelState.IsValid)
                {
                    if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                    {
                        if (System.IO.File.Exists(Server.MapPath("~/Content/images/members/" + _member.IMAGE)))
                            System.IO.File.Delete(Server.MapPath("~/Content/images/members/" + _member.IMAGE));

                        string dosyaAdi = Guid.NewGuid().ToString().Replace("-", "");
                        string uzanti = Path.GetExtension(Request.Files[0].FileName);
                        string tamYolYeri = "~/Content/images/members/" + dosyaAdi + uzanti;
                        Request.Files[0].SaveAs(Server.MapPath(tamYolYeri));
                        member.IMAGE = dosyaAdi + uzanti;
                    }
                    else
                        member.IMAGE = _member.IMAGE;

                    memberDA.Update(member);
                    return RedirectToAction("Members", "Admin");
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    PopulateStateDropdownList(_member.STATE_ID);
                    return View("MemberUpdate", _member);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                PopulateStateDropdownList(_member.STATE_ID);
                return View("MemberUpdate", _member);
            }
        }

        [Route("admin/takim-uyesi-sil/{id}")]
        public ActionResult MemberDelete(int id)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            MEMBER member = memberDA.GetById(id);
            return View(member);
        }

        [HttpPost]
        public ActionResult DeleteMember(int id)
        {
            MEMBER member = memberDA.GetById(id);
            try
            {
                if (ModelState.IsValid)
                {
                    memberDA.Delete(id);

                    if (System.IO.File.Exists(Server.MapPath("~/Content/images/members/" + member.IMAGE)))
                        System.IO.File.Delete(Server.MapPath("~/Content/images/members/" + member.IMAGE));

                    return RedirectToAction("Members", "Admin");
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    return View("MemberDelete", member);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                return View("MemberDelete", member);
            }
        }

        #endregion

        #region İletişim Bilgileri
        [Route("admin/iletisim-bilgileri")]
        public ActionResult ContactInfo()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            CONTACTINFO contactInfo = contactInfoDA.Get();
            if (contactInfo.ID <= 0)
            {
                contactInfo.PHONE = "";
                contactInfo.EMAIL = "";
                contactInfo.ADDRESS = "";
                contactInfo.FACEBOOK = "";
                contactInfo.TWITTER = "";
                contactInfo.INSTAGRAM = "";
                contactInfoDA.Insert(contactInfo);

                contactInfo = contactInfoDA.Get();
            }
            return View(contactInfo);
        }

        [HttpPost]
        public ActionResult UpdateContactInfo(CONTACTINFO contactInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contactInfoDA.Update(contactInfo);
                    return RedirectToAction("ContactInfo", "Admin");
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    return View("ContactInfo", contactInfo);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                return View("ContactInfo", contactInfo);
            }
        }

        #endregion

        #region İletişim Formları

        [Route("admin/iletisim-formlari")]
        public ActionResult ContactForms()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            List<CONTACTFORM> contactForms = contactFormDA.GetAll();
            return View(contactForms);
        }

        [Route("admin/iletisim-formu/{id}")]
        public ActionResult ContactForm(int id)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            CONTACTFORM contactForm = contactFormDA.GetById(id);
            return View(contactForm);
        }

        [Route("admin/iletisim-formu-sil/{id}")]
        public ActionResult ContactFormDelete(int id)
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            CONTACTFORM contactForm = contactFormDA.GetById(id);
            return View(contactForm);
        }

        [HttpPost]
        public ActionResult DeleteContactForm(int id)
        {
            CONTACTFORM contactForm = contactFormDA.GetById(id);
            try
            {
                if (ModelState.IsValid)
                {
                    contactFormDA.Delete(id);

                    return RedirectToAction("ContactForms", "Admin");
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    return View("ContactFormDelete", contactForm);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                return View("ContactFormDelete", contactForm);
            }
        }

        #endregion


        [Route("admin/sifre-degistir")]
        public ActionResult ChangePassword()
        {
            if (Session["ID"] == null)
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public ActionResult UpdatePassword(CHANGEPASSWORD changePassword)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int userId = (int)Session["ID"];
                    USER user = userDA.GetById(userId);

                    if (user.PASSWORD == changePassword.OLDPASSWORD)
                    {
                        if (changePassword.NEWPASSWORD == changePassword.NEWPASSWORD_REPEAT)
                        {
                            userDA.ChangePassword(userId, changePassword.NEWPASSWORD);
                            ViewBag.Success = "Şife değiştirildi.";
                            return View("ChangePassword");
                        }
                        else
                        {
                            ViewBag.Error = "Yeni şifreler uyuşmuyor.";
                            return View("ChangePassword");
                        }
                    }
                    else
                    {
                        ViewBag.Error = "Eski şifre hatalı.";
                        return View("ChangePassword");
                    }
                }
                else
                {
                    ViewBag.Error = "Try Again.";
                    return View("ChangePassword");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Try Again.";
                return View("ChangePassword");
            }
        }

        private void PopulateStateDropdownList(object selectedState = null)
        {
            var statesQuery = stateDA.GetAll();
            ViewBag.STATE_ID = new SelectList(statesQuery, "ID", "NAME", selectedState);
        }

        private void PopulateCategoryDropdownList(object selectedCategory = null)
        {
            var categoriesQuery = categoryDA.GetAll(true);
            ViewBag.CATEGORY_ID = new SelectList(categoriesQuery, "ID", "NAME", selectedCategory);
        }
    }
}