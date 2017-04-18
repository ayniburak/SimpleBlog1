using NHibernate.Cfg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBlog2
{
    public static class Database
    {
        public static void Configure()//APP Level - Only Once //bu sadece bir kez app startda çağırılıyor.
        {
            var config = new Configuration();
            config.Configure();
            //select  connection string

            //add mapping

            //create session factory
        }
        public static void OpenSession()//Request Level - per request
        {

        }
        public static void CloseSession()//Request Level - per request
        {

        }
    }
}