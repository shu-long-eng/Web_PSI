using System;


namespace PSIsystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Msg.Visible = false;
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            string Account = this.AccountText.Text;//獲得帳號
            string PWD = this.PWDText.Text;//獲得密碼
            if(!Helpers.LoginHelper.TryLogin(Account, PWD))//登入失敗
            {
                this.Msg.Visible = true;//顯示錯誤訊息
            }
            else//登入成功
            {
                
                Response.Redirect("PurchasePage.aspx");//跳轉進訂單一覽
               
            }
        }
    }
}