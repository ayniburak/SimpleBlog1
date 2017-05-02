using NHibernate.Linq;
using SimpleBlog2.Models;
using SimpleBlog2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleBlog2.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()  //bu get isteği
        {
            
            return View(new AuthLogin());
        }

        [HttpPost] //bunun varlıgı altındaki fonksiyonun post olduğu zaman çalışacağını belirler.
        public ActionResult Login(AuthLogin form,string returnUrl) //bu post isteği yukarıdaki http kısmından form getirmesini saglıyor
        {

            var user = Database.Session.Query<User>().FirstOrDefault(u => u.Username == form.Username);
            if (user == null)
            {
                SimpleBlog2.Models.User.FakeHash();
            }
            if (user == null || !user.CheckPassword(form.Password))
            {
                ModelState.AddModelError("Username", "Username or password is invalid !");

            }
            //return Content("Hi "+form.Username+" - your password : " + form.Password); 13.03.17 dersinde kaldırıldı view e döndürüldü
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (form.Username.Length < 5)
            {
                ModelState.AddModelError("Username","Username must be 5 characters at least");
                return View(form); 
            }
            FormsAuthentication.SetAuthCookie(form.Username, true);
            if (!string.IsNullOrWhiteSpace(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToRoute("Home");
            }
            return Content("The form is valid");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("Home");
        }
    }
}   