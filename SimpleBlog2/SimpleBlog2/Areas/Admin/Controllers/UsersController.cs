using NHibernate.Linq;
using SimpleBlog2.Areas.Admin.ViewModels;
using SimpleBlog2.Infrastructure;
using SimpleBlog2.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SimpleBlog2.Areas.Admin.Controllers
{
    
    [Authorize(Roles = "admin")]
    [SelectedTabAttribute("users")]
    public class UsersController : Controller
    {
        private void SyncRoles(IList<RoleCheckBox> checkBoxes, IList<Role> roles)
        {
            var selectedRoles = new List<Role>();
            foreach (var role in Database.Session.Query<Role>())
            {
                var checkBox = checkBoxes.Single(c => c.Id == role.Id);
                checkBox.Name = role.Name;
                if (checkBox.IsChecked)
                    selectedRoles.Add(role);
            }
            foreach (var toAdd in selectedRoles.Where(t => !roles.Contains(t)))
                roles.Add(toAdd);
            foreach (var toRemove in roles.Where(t => !selectedRoles.Contains(t)).ToList())
                roles.Remove(toRemove);
        }
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
            return View(new UsersNew()
            {
                Roles = Database.Session.Query<Role>().Select(role => new RoleCheckBox
                {
                    Id = role.Id,
                    IsChecked=false,
                    Name = role.Name
                }).ToList()
                
            });
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
            SyncRoles(form.Roles, user.Roles);
            user.SetPassword(form.Password);
            Database.Session.Save(user);
            return RedirectToAction("index");
        }
        public ActionResult Edit(int id)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new UsersEdit() {
                Username = user.Username,
                Email = user.Email,
            Roles = Database.Session.Query<Role>().Select(role =>  new RoleCheckBox
            {
                Id = role.Id,
                IsChecked =user.Roles.Contains(role),
                Name = role.Name
            }).ToList()
            });
            
            
        }
        [HttpPost]
        public ActionResult Edit(int id,UsersEdit form)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            if (Database.Session.Query<User>().Any(p => p.Username == form.Username && p.Id!=id))
            {
                ModelState.AddModelError("Username", "Username must be unique");
            }
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            user.Username = form.Username;
            user.Email = form.Email;
            //var user = new User()
            //{
            //    Username = form.Username,
            //    Email = form.Email
            //};
            SyncRoles(form.Roles, user.Roles);
            Database.Session.Update(user);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
        public ActionResult ResetPassword(int id)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(new UsersResetPassword()
            {
                Username = user.Username
                
            });
        }
        [HttpPost]
        public ActionResult ResetPassword(int id, UsersResetPassword form)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }

            form.Username = user.Username;
            if (!ModelState.IsValid)
            {
                return View(form);
            }
            user.SetPassword(form.Password);
            

            Database.Session.Update(user);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = Database.Session.Query<User>().FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                return HttpNotFound();
            }
            Database.Session.Delete(user);
            Database.Session.Flush();
            return RedirectToAction("index");
        }
    }
}