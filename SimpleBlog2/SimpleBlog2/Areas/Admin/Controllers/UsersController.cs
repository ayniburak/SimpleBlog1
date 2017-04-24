using NHibernate.Linq;
using SimpleBlog2.Areas.Admin.ViewModels;
using SimpleBlog2.Infrastructure;
using SimpleBlog2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleBlog2.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [SelectedTabAttribute("users")]
    public class UsersController : Controller
    {
        // GET: Admin/Users
        public ActionResult Index()
        {
            var users = Database.Session.Query<User>();
            return View(new UsersIndex() { Users = users });
        }
        public ActionResult List()
        {
            return Content("Admin Area Users Controller List Action");
        }
        public ActionResult Edit(int id = 0)
        {
            return Content("Admin Area Users Controller Edit Action : " + id.ToString());//bunların içeriğini biz yazıyoruz
        }
        public ActionResult brk(int id = 0)
        {
            return Content("Admin Area Users Controller brk Action : " + id.ToString());//bunların içeriğini biz yazıyoruz
        }
        public ActionResult okn(int id = 0)
        {
            return Content("Admin Area Users Controller okn Action : " + id.ToString());//bunların içeriğini biz yazıyoruz
        }

        public ActionResult New()
        {
            return View(new UsersNew() { });
        }
        [HttpPost]
        public ActionResult New(UsersNew form)
        {
            if (Database.Session.Query<User>().Any(p => p.Username == form.Username))
            {
                ModelState.AddModelError("Username", "Username must be unique");
            }
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            var user = new User()
            {
                Username = form.Username,
                Email = form.Email
            };
            user.SetPassword(form.Password);
            Database.Session.Save(user);
            return RedirectToAction("index");
        }
    }
}