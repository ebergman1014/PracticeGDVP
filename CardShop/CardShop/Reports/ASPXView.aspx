<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html>
<head id="Head1" runat="server">
    <title>ASPXView</title>
</head>
<body>
    <div>
        <script runat="server">
            private void Page_Load(object sender, System.EventArgs e)
            {
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/AuditingLogs2/AuditReport.rdlc");
                ReportViewer1.LocalReport.Refresh();
            }
        </script>
        <form id="Form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="500" Width="500" AsyncRendering="false"></rsweb:ReportViewer>
        </form>
    </div>
</body>
</html>
