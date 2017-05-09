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
    [Authorize(Roles ="admin")]
    [SelectedTabAttribute("posts")]
    public class PostsController : Controller
    {
        // GET: Admin/Posts
        private const int PostPerPage = 10;
        public ActionResult Index(int page = 1)
        {
            var totalPostCount = Database.Session.Query<Post>().Count();
            var currentPostPage = Database.Session.Query<Post>()
                .OrderByDescending(c => c.CreatedAt)
                .Skip((page - 1) * PostPerPage)
                .Take(PostPerPage)
                .ToList();
            return View(new PostsIndex()
            {
                Posts = new PagedData<Post>(currentPostPage, totalPostCount, page, PostPerPage)
            });

        }
        public ActionResult New()
        {
            return View("Form", new PostsForm() { IsNew = true });
        }

        public ActionResult Edit(int id)
        {
            var post = Database.Session.Load<Post>(id);
            if (post == null)
            {
                return HttpNotFound();
            }


            return View("Form", new PostsForm() { IsNew = false, PostId = post.Id, Tittle = post.Title, Content = post.Content, Slug = post.Slug });
        }

        [HttpPost]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
                return View(form);

            Post post;

            if (form.IsNew)
            {
                post = new Post()
                {
                    Title = form.Tittle,
                    Slug = form.Slug,
                    Content = form.Content,
                    CreatedAt = DateTime.UtcNow,
                    User = Auth.User

                };
            }
            else
            {
                post = Database.Session.Load<Post>(form.PostId);

                if (post == null)
                {
                    return HttpNotFound();
                }

                post.Title = form.Tittle;
                post.Slug = form.Slug;
                post.Content = form.Content;
                post.UpdatedAt = DateTime.UtcNow;
            }

            Database.Session.SaveOrUpdate(post);

            Database.Session.Flush();
            return RedirectToAction("Index");
        }
    }
}
