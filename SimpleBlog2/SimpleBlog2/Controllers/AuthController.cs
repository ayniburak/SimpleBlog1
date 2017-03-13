using SimpleBlog2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog2.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()  //bu get isteği
        {
            
            return View(new AuthLogin());
        }

        [HttpPost] //bunun varlıgı altındaki fonksiyonun post olduğu zaman çalışacağını belirler.
        public ActionResult Login(AuthLogin form) //bu post isteği yukarıdaki http kısmından form getirmesini saglıyor
        {
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
            return Content("The form is valid");
        }
    }
}   