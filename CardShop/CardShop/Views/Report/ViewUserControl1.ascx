<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<script runat="server">
    private void Page_Load(object sender, System.EventArgs e)
    {
        ReportViewer2.LocalReport.ReportPath = Server.MapPath("~/AuditingLogs2/AuditReport.rdlc");
        ReportViewer2.LocalReport.Refresh();
    }
</script>
<form id="Form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer2" runat="server" AsyncRendering="false"></rsweb:ReportViewer>
</form>
