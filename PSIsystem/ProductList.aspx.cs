using PSIsystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSIsystem
{
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //獲得商品一覽
            this.Product.DataSource = DB.ShowProduct();
            this.Product.DataBind();
        }
    }
}