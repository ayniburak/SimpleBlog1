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
            return View();
        }

        [HttpPost] //bunun varlıgı altındaki fonksiyonun post olduğu zaman çalışacağını belirler.
        public ActionResult Login(AuthLogin form) //bu post isteği yukarıdaki http kısmından form getirmesini saglıyor
        {
            return Content("Hi "+form.Username+" - your password : " + form.Password);
        }
    }
}   