using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSIsystem.PageManager
{
    public class totalpage
    {
        public static int totalsize()
        {
            using(var context = new Models.PSIsystemModel())
            {
                var query = context.Purchases.Where(item=>item.Isdelete!=true).GroupBy(item => item.ID).Select(item => item.Key);
                return query.Count();
            }
        }
    }
}