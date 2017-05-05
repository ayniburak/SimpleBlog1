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
    }
}