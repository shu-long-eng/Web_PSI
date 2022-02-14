using PSIsystem.Helpers;
using PSIsystem.PageManager;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace PSIsystem
{
    public partial class PurchasePage : System.Web.UI.Page
    {
        static List<string> IDList = new List<string>();//IDList儲存Checkbox所選取的ID
        private int _pageSize = 5;//設定_pageSize為一個分頁的項目個數為5
        internal class PagingLink//分頁所需要的資訊
        {
            public string Name { get; set; } //表示第幾頁
            public string Link { get; set; } //表示該分頁的連結
            public string Title { get; set; } //表示該分頁的標題
        }
        protected void Page_Init(object sender, EventArgs e)
        {
            
            LoadGridView();
        }

        private void LoadGridView()
        {
            //----- Get Query string parameters -----
            string page = Request.QueryString["Page"];//取得當前頁數
            int pIndex = 0;
            if (string.IsNullOrEmpty(page))
                pIndex = 1;
            else
            {
                int.TryParse(page, out pIndex);

                if (pIndex <= 0)
                    pIndex = 1;
            }



            int totalSize = PageManager.totalpage.totalsize();//取得Data個數

            List<PurchasesInfo> list = DB.ShowPurchase(_pageSize, pIndex);//取得分頁的資料

            int pages = CalculatePages.myCalculatePages(totalSize, _pageSize);//取得分頁頁數

            List<PagingLink> pagingList = new List<PagingLink>();



            for (var i = 1; i <= pages; i++)
            {
                pagingList.Add(new PagingLink()
                {
                    Link = $"PurchasePage.aspx{this.GetQueryString(false, i)}",
                    Name = $"{i}",
                    Title = $"前往第 {i} 頁"
                });


            }

            this.repPaging.DataSource = pagingList;
            this.repPaging.DataBind();

            this.OrderRepeater.DataSource = list;
            this.OrderRepeater.DataBind();

        }

        //儲存每一個頁面的QueryString
        private string GetQueryString(bool includePage, int? pageIndex)
        {
            //----- Get Query string parameters -----
            string page = Request.QueryString["Page"];


            //----- Get Query string parameters -----


            List<string> conditions = new List<string>();

            if (!string.IsNullOrEmpty(page) && includePage)
                conditions.Add("Page=" + page);

            if (pageIndex.HasValue)
                conditions.Add("Page=" + pageIndex.Value);

            string retText =
                (conditions.Count > 0)
                    ? "?" + string.Join("&", conditions)
                    : string.Empty;

            return retText;
        }

        protected void ItemCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var ItemCheckBox = sender as CheckBox;
            //儲存CheckBox所選取的ID
            if (ItemCheckBox.Checked) { 
                string ID = ItemCheckBox.Text;
                IDList.Add(ID);
            }else if (!ItemCheckBox.Checked)
            {
                string ID = ItemCheckBox.Text;
                IDList.Remove(ID);
            }
        }

        protected void Delete_Click(object sender, EventArgs e)
        {
            if(IDList.Count == 0)
            {
                return;
            }
            DB.DeletePurchase(IDList);
            IDList.Clear();//刪除後清除IDList所儲存的ID
            LoadGridView();//重新取的更新後的資料
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            //前往新增訂單頁面
            Response.Redirect("PurchaseAdd.aspx");
        }

        protected void Print_Click(object sender, EventArgs e)
        {
            //前往報表頁
            Response.Redirect("Report.aspx");
            
        }
    }
}