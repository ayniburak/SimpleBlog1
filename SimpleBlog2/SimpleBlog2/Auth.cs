using NHibernate.Linq;
using SimpleBlog2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog2
{
    public static class Auth
    {
        public static User User {
            get {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return null;
                }
                var user = HttpContext.Current.Items["UserKey"] as User;
                if (user == null)
                {
                    user = Database.Session.Query<User>().FirstOrDefault(p => p.Username == HttpContext.Current.User.Identity.Name);
                    if (user == null)
                    {
                        return null;
                    }
                    HttpContext.Current.Items["UserKey"] = user;
                }
                return user;
            }
        }
    }
}