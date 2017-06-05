<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="FacebookLoginASPnetWebForms.account.user" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../analyticstracking.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem>IAtest</asp:ListItem>
                <asp:ListItem>IAtesting</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:Chart ID="Chart1" runat="server" Width="900px">
                <series>
                    <asp:Series Name="Series1" ChartType="Line">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </chartareas>
            </asp:Chart>
        </div>
    </form>
</body>
</html>
