<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="FacebookLoginASPnetWebForms.account.user" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script>

  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){
  (i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),
  m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)
  })(window,document,'script','https://www.google-analytics.com/analytics.js','ga');

  ga('create', 'UA-82121856-1', 'auto');
  ga('send', 'pageview');

</script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <asp:ListView ID="ListView1" runat="server">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td>id:</td>
                            <td><%# Eval("id") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>about:</td>
                            <td><%# Eval("about") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>bio:</td>
                            <td><%# Eval("bio") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>birthday:</td>
                            <td><%# Eval("birthday") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>cover:</td>
                            <td><%# Eval("cover") %><br />
                            </td>
                        </tr>
<%--                        <tr>
                            <td>education:</td>
                            <td><%# Eval("education") %><br />
                            </td>
                        </tr>--%>
                        <tr>
                            <td>devices:</td>
                            <td><%# Eval("devices") %><br />
                            </td>
                        </tr>
                         <tr>
                            <td>email:</td>
                            <td><%# Eval("email") %><br />
                            </td>
                        </tr>
<%--                                                <tr>
                            <td>favorite_athletes:</td>
                            <td><%# Eval("favorite_athletes") %><br />
                            </td>
                        </tr>--%>
<%--                        <tr>
                            <td>favorite_teams:</td>
                            <td><%# Eval("favorite_teams") %><br />
                            </td>
                        </tr>--%>
                         <tr>
                            <td>first_name:</td>
                            <td><%# Eval("first_name") %><br />
                            </td>
                        </tr>
                                                <tr>
                            <td>gender:</td>
                            <td><%# Eval("gender") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>install_type:</td>
                            <td><%# Eval("install_type") %><br />
                            </td>
                        </tr>
                         <tr>
                            <td>installed:</td>
                            <td><%# Eval("installed") %><br />
                            </td>
                        </tr>
<%--                                                <tr>
                            <td>interested_in:</td>
                            <td><%# Eval("interested_in") %><br />
                            </td>
                        </tr>--%>
                        <tr>
                            <td>is_shared_login:</td>
                            <td><%# Eval("is_shared_login") %><br />
                            </td>
                        </tr>
                         <tr>
                            <td>is_verified:</td>
                            <td><%# Eval("is_verified") %><br />
                            </td>
                        </tr>
<%--                                                <tr>
                            <td>languages:</td>
                            <td><%# Eval("languages") %><br />
                            </td>
                        </tr>--%>
                        <tr>
                            <td>last_name:</td>
                            <td><%# Eval("last_name") %><br />
                            </td>
                        </tr>
                         <tr>
                            <td>link:</td>
                            <td><%# Eval("link") %><br />
                            </td>
                        </tr>
                                                <tr>
                            <td>locale:</td>
                            <td><%# Eval("locale") %><br />
                            </td>
                        </tr>
                       
<%--                        <tr>
                            <td>location:</td>
                            <td><%# Eval("page.location") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>hometown:</td>
                            <td><%# Eval("page") %><br />
                            </td>
                        </tr>--%>
<%--                         <tr>
                            <td>meeting_for:</td>
                            <td><%# Eval("meeting_for") %><br />
                            </td>
                        </tr>--%>
                                                <tr>
                            <td>middle_name:</td>
                            <td><%# Eval("middle_name") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>name:</td>
                            <td><%# Eval("name") %><br />
                            </td>
                        </tr>
                         <tr>
                            <td>name_format:</td>
                            <td><%# Eval("name_format") %><br />
                            </td>
                        </tr>
                                                <tr>
                            <td>political:</td>
                            <td><%# Eval("political") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>public_key:</td>
                            <td><%# Eval("public_key") %><br />
                            </td>
                        </tr>
                         <tr>
                            <td>quotes:</td>
                            <td><%# Eval("quotes") %><br />
                            </td>
                        </tr>
                                                <tr>
                            <td>relationship_status:</td>
                            <td><%# Eval("relationship_status") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>religion:</td>
                            <td><%# Eval("religion") %><br />
                            </td>
                        </tr>
                         <tr>
                            <td>shared_login_upgrade_required_by:</td>
                            <td><%# Eval("shared_login_upgrade_required_by") %><br />
                            </td>
                        </tr>
<%--                                                <tr>
                            <td>significant_other:</td>
                            <td><%# Eval("significant_other") %><br />
                            </td>
                        </tr>--%>
<%--                        <tr>
                            <td>sports:</td>
                            <td><%# Eval("sports") %><br />
                            </td>
                        </tr>--%>
                         <tr>
                            <td>test_group:</td>
                            <td><%# Eval("test_group") %><br />
                            </td>
                        </tr>
                                                <tr>
                            <td>third_party_id:</td>
                            <td><%# Eval("third_party_id") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>timezone:</td>
                            <td><%# Eval("timezone") %><br />
                            </td>
                        </tr>
                         <tr>
                            <td>updated_time:</td>
                            <td><%# Eval("updated_time") %><br />
                            </td>
                        </tr>
                                                <tr>
                            <td>verified:</td>
                            <td><%# Eval("verified") %><br />
                            </td>
                        </tr>
                        <tr>
                            <td>viewer_can_send_gift:</td>
                            <td><%# Eval("viewer_can_send_gift") %><br />
                            </td>
                        </tr>
                         <tr>
                            <td>website:</td>
                            <td><%# Eval("website") %><br />
                            </td>
                        </tr>
<%--                                                 <tr>
                            <td>work:</td>
                            <td><%# Eval("work") %><br />
                            </td>
                        </tr>--%>

                    </table>
                </ItemTemplate>
            </asp:ListView>
            <br />
            <asp:Chart ID="Chart1" runat="server">
                <series>
                    <asp:Series Name="Series1">
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
