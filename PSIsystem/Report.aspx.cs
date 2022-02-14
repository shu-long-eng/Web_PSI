using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System;
using System.Linq;


namespace PSIsystem
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            using(var context = new Models.PSIsystemModel())
            {   //取出資料
                var query = context.Purchases.Where(item => item.Isdelete != true)
                    .GroupBy(item => new { item.ID, item.Date, item.Creator })
                    .Select(item => new
                    {
                        ID = item.Key.ID,
                        CountID = item.Count(),
                        Count = (int)item.Sum(x => x.Count),
                        Date = (DateTime)item.Key.Date,
                        Total = (decimal)item.Sum(x => x.Total),
                        Creator = item.Key.Creator

                    }).ToList();
                //匯入水晶報表資料
                ReportDocument crp = new ReportDocument();
                crp.Load(Server.MapPath("CrystalReport1.rpt"));
                crp.SetDataSource(query);
                this.CrystalReportViewer1.ReportSource = crp;
                crp.ExportToHttpResponse(ExportFormatType.PortableDocFormat,
                    Response, false, "Purchase List");
            }




           
        }
    }
}