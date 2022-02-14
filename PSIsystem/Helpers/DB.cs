using System;
using System.Collections.Generic;
using System.Linq;


namespace PSIsystem.Helpers
{
    public class DB
    {
        public static List<PurchasesInfo> ShowPurchase(int pageSize, int currentPage)
        {
            using (var context = new Models.PSIsystemModel())
            {
                
                List<PurchasesInfo> list = new List<PurchasesInfo>();
                var query = from item in context.Purchases
                            where item.Isdelete != true
                            group item by new { item.ID, item.Date } into gro
                            orderby gro.Key.ID
                            select new
                            {
                                ID = gro.Key.ID,
                                ProductType = gro.Count(),
                                Count = gro.Sum(item => item.Count),
                                Date = gro.Key.Date,
                                Total = gro.Sum(item => item.Total)
                            };

                var result = query.Skip(pageSize * (currentPage-1)).Take(5);
                foreach (var item in result)
                {
                    list.Add(new PurchasesInfo()
                    {
                        ID = item.ID,
                        ProductType = item.ProductType,
                        Count = (int)item.Count,
                        Date = (DateTime)item.Date,
                        Total = (decimal)item.Total
                    });
                }
                return list;
            }

        }
        public static void DeletePurchase(List<string> list)
        {
            using(var context = new Models.PSIsystemModel())
            {
                foreach(var ID in list)
                {
                    var query = context.Purchases.Where(item => item.ID == ID);
                    foreach (var item in query)
                    {
                        item.Isdelete = true;
                    }
                }

                context.SaveChanges();
            }
        }
        public static int CountData()
        {
           using(var context = new Models.PSIsystemModel())
            {
                var query = from item in context.Purchases
                            group item by item.ID ;

                return query.Count();
            }

        }
        public static List<ProductInfo> ShowProduct()
        {
            using(var context = new Models.PSIsystemModel())
            {
                var query = context.Products.Select(item =>new { 
                    ID = item.ID,
                    Name = item.Name,
                    Price = item.Price,
                    ProductType = item.Type
                });

                List<ProductInfo> list = new List<ProductInfo>();

                foreach(var item in query)
                {
                    list.Add(new ProductInfo()
                    {
                        ID = item.ID,
                        Name = item.Name,
                        Price = (decimal)item.Price,
                        ProductType = item.ProductType
                    });
                }
                return list;
            }
        }
        
        
    }
}