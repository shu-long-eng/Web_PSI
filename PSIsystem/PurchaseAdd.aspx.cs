using PSIsystem.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace PSIsystem
{
    public partial class PurchaseAdd : System.Web.UI.Page
    {

       
        static List<Order> list = new List<Order>();//創建list暫存訂單資料
        private const string _sessionKey = "IsLogined";
        protected void Page_Load(object sender, EventArgs e)
        {   //取得單號
            int countID = Helpers.DB.CountData()+1;
            string SubID = string.Format("{0:D3}", countID);
            string ID = "ASN-"+ SubID;
            this.IDText.Text = ID;//設定IDText為單號
            //取得下拉選單項目
            if (!IsPostBack) { 
            List<ProductInfo> list = DB.ShowProduct();
            foreach (var item in list)
            {
                this.ProductList.Items.Add(new ListItem(item.Name, item.ID+" "+item.Price));//設定下拉選單的選項Text為Product Table的Name欄位,value為ID+Price欄位
            };
            }
            string[] arr = this.ProductList.SelectedItem.Value.Split(' ');//把ID及Price分開arr[0]為ID,arr[1]為Price
            this.Price.Text = arr[1];
            //把小計欄位設定為唯讀
            this.Total.Attributes.Add("readonly","true");

            this.CreateTime.Text = DateTime.Now.ToString();
            LoginInfo AccountInfo = (LoginInfo)HttpContext.Current.Session[_sessionKey];
            this.Creator.Text = AccountInfo.Name.ToString();
        }

        protected void ProductList_SelectedIndexChanged(object sender, EventArgs e)//下拉選單選擇項目
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#Order').modal('show')", true);//讓彈跳視窗不會因為PostBack消失
            this.Count.Text = "0";//下拉選單選擇不同選項時，讓數量歸零
        }

        protected void AddProduct_Click(object sender, EventArgs e)//新增商品
        {
            //把欄位資料暫存在變數中
            string[] arr = this.ProductList.SelectedItem.Value.Split(' ');
            string ID = arr[0]; //ID設定為商品ID
            string Name = this.ProductList.SelectedItem.Text;//Name設定為商品名稱
            string Price = arr[1];//Price設定為商品價格
            string Count = this.Count.Text;//Count設定為商品數量
            string Total = this.Total.Text;//Total設定為商品小計





            if (Count=="0"||Total == "0")
            {
                Response.Write("<script>alert('數量不可為0')</script>");
                return;
            }
            //list儲存商品資訊
            list.Add(new Order() { 
                ID = ID,
                Name = Name,
                Price = Convert.ToDecimal(Price),
                Count = Convert.ToInt32(Count),
                Total = Convert.ToDecimal(Total),
            });
            //如果有相同商品重複下訂，把金額跟數量相加
            var query = list.GroupBy(item => new { item.ID, item.Name, item.Price })
                        .OrderBy(item=>item.Key.ID)
                        .Select(item => new { 
                            ID = item.Key.ID,
                            Name = item.Key.Name,
                            Count = item.Sum(x=>x.Count),
                            Price = item.Key.Price.ToString("#,##0"),
                            Total = item.Sum(x=>x.Total).ToString("#,##0")
                        });
            //資料綁訂
            this.ProductRepeater.DataSource = query;
            this.ProductRepeater.DataBind();

            //綁訂後所有欄位初始化
            this.ProductList.SelectedIndex = 0;
            arr = this.ProductList.SelectedItem.Value.Split(' ');
            this.Price.Text = arr[1];
            this.Count.Text = "0";
            this.Total.Text = "0";
            //把所有小計總和後把值賦予總計
            decimal TotalMoney = 0;
            foreach (var item in query)
            {
                TotalMoney = TotalMoney + Convert.ToDecimal(item.Total);
            }
            this.TotalMoney.Text = TotalMoney.ToString("#,##0");
        }

        

        protected void ProductRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)//更新訂單頁面資訊
        {   //取得欲刪除的項目ID儲存進變數ID
            string ID = e.CommandArgument.ToString();
            //把所有ID為變數ID的項目刪除
            list.RemoveAll(item => item.ID == ID);

            var query = list
                .GroupBy(item => new { item.ID, item.Name, item.Price })
                .OrderBy(item => item.Key.ID)
                .Select(item => new {
                        ID = item.Key.ID,
                        Name = item.Key.Name,
                        Count = item.Sum(x => x.Count),
                        Price = item.Key.Price.ToString("#,##0"),
                        Total = item.Sum(x => x.Total).ToString("#,##0")
                    });
            //資料綁訂
            this.ProductRepeater.DataSource = query;
            this.ProductRepeater.DataBind();
            //把小計相加把值賦予總計
            int TotalMoney = 0;
            foreach (var item in list)
            {
                TotalMoney = TotalMoney + Convert.ToInt32(item.Total);
            }
            this.TotalMoney.Text = TotalMoney.ToString("#,##0");
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("PurchasePage.aspx");//取消後回到訂單一覽
        }

        protected void SaveBtn_Click(object sender, EventArgs e)//儲存訂單
        {
            string PurchaseDate = this.PurchaseDate.Text;//PurchaseDate設定為訂單日期
            string PurchaseTime = this.PurchaseTime.Text;//PurchaseTime設定為訂單時間
            string PurchaseID = this.IDText.Text;//訂單ID
            string Creator = this.Creator.Text;//創建者
            DateTime CreateTime = Convert.ToDateTime(this.CreateTime.Text);//創建時間

            //判斷有無輸入日期、時間、商品
            if (string.IsNullOrEmpty(PurchaseDate) || string.IsNullOrEmpty(PurchaseTime))
            {
                Response.Write("<script>alert('請輸入日期及時間')</script>");
                return;
            }
            if (list.Count == 0)
            {
                Response.Write("<script>alert('請新增商品')</script>");
                return;
            }

            string DateTime = PurchaseDate +" "+PurchaseTime;//訂單日期跟訂單時間合起來儲存成完整的DateTime
            DateTime PurchaseDateTime = Convert.ToDateTime(DateTime);//DateTime轉型儲存進PurchaseDateTime

            var query = list.GroupBy(item => new { item.ID, item.Name, item.Price })
                .OrderBy(item => item.Key.ID)
                .Select(item => new
                {
                    PurchaseID = PurchaseID,
                    ID = item.Key.ID,
                    Name = item.Key.Name,
                    Count = item.Sum(x => x.Count),
                    Price = item.Key.Price,
                    Total = item.Sum(x => x.Total),
                    Creator = Creator,
                    CreateTime = CreateTime,
                    PurchaseTime = PurchaseDateTime
                });
                //儲存進資料庫
            using(var context = new Models.PSIsystemModel())
            {
                foreach(var item in query)
                {
                    var NewOrder = new Models.Purchase()
                    {
                        ID = item.PurchaseID,
                        ProductID = item.ID,
                        Count = item.Count,
                        Date = item.PurchaseTime,
                        Total = item.Total,
                        Creator = item.Creator,
                        CreateTime = item.CreateTime,
                        Isdelete = false
                    };
                    context.Purchases.Add(NewOrder);
                    
                }
                context.SaveChanges();

            }
            list.Clear();//清除暫存資料
            Response.Redirect("PurchasePage.aspx");//回到訂單一覽
        }
    }
}