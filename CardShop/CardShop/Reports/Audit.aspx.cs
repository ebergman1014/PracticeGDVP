using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CardShop.Reports
{
    public partial class Audit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //AuditReport.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                //AuditReport.ServerReport.ReportServerUrl = new Uri("http://PC20181/ReportServer");
                //AuditReport.ServerReport.ReportPath = "/AuditingLogs2/AuditReport";
                //AuditReport.ServerReport.Refresh();
            }
        }
    }
}