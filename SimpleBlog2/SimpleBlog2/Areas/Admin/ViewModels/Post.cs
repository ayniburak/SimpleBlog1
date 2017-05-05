using SimpleBlog2.Infrastructure;
using SimpleBlog2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog2.Areas.Admin.ViewModels
{
    
    public class PostsIndex
    {
        public PagedData<Post> Posts { get; set; }
    }
}