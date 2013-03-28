<%@ Page Language="C#" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">

        <div>
            <%--<iframe seamless="seamless" width="1250">--%>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>

                <rsweb:ReportViewer ID="AuditReport" runat="server" ProcessingMode="Remote" Width="1200px">
                    <ServerReport DisplayName="Audit Report" ReportServerUrl="http://PC20181/ReportServer" ReportPath="/AuditingLogs2/AuditReport" />
                </rsweb:ReportViewer>
            <%--</iframe>--%>
        </div>
    </form>

</body>
</html>
