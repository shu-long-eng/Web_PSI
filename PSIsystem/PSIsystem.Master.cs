using PSIsystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PSIsystem
{
    public partial class PSIsystem : System.Web.UI.MasterPage
    {
        protected void Page_init(object sender, EventArgs e)
        {
            //判斷有無登入，如果未登入進入登入畫面，如果已登入獲得帳號資訊
            if (!Helpers.LoginHelper.HasLogined())
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                LoginHelper.GetCurrentUserInfo();
            }
        }
        
        protected void Logout_ServerClick(object sender, EventArgs e)//登出
        {
            Session.Clear();//清除Session
            Response.Redirect("Login.aspx");//回到燈入夜
        }
    }
}