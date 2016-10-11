<%@ Page Language="VB" MasterPageFile="~/_layouts/application.master" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral,PublicKeyToken=71e9bce111e9429c"%> 
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.WebControls" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBar" src="~/_controltemplates/ToolBar.ascx" %>
<%@ Register TagPrefix="wssuc" TagName="ToolBarButton" src="~/_controltemplates/ToolBarButton.ascx" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea">
Content Types Hierarchy
</asp:Content>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="PlaceHolderPageDescription">
This page shows all Site Content Types and their hierarchical relationships
</asp:Content>

<asp:Content ID="Content4" runat="server" ContentPlaceHolderID="PlaceHolderMain" >
<TABLE border="0" width="100%" cellspacing="0" cellpadding="0">
  <TR>
    <TD ID="mngfieldToobar">
      <wssuc:ToolBar id="onetidMngFieldTB" runat="server">
      <Template_Buttons>
          <wssuc:ToolBarButton runat="server" Text="<%$Resources:wss,multipages_createbutton_text%>" id="idAddField" ToolTip="<%$Resources:wss,mngctype_create_alt%>" NavigateUrl="ctypenew.aspx" ImageUrl="/_layouts/images/newitem.gif" AccessKey="C" />
        </Template_Buttons>
      </wssuc:ToolBar>
    </TD>
  </TR>
  </TABLE>

<%
    
    'System Content Type is the root
    Dim objSite As SPSite = SPControl.GetContextSite(Context)
    Dim objTypes As SPContentTypeCollection = objSite.OpenWeb().ContentTypes
    Dim objID As SPContentTypeId = objTypes(0).Id
    Response.Write("<table style='font-size:10pt' border='0'" + " cellpadding='2' width='50%'><tr><td>")
    Response.Write("<li><a class='ms-topnav'" & " href=""/_layouts/ManageContentType.aspx?ctype=" & _
    objTypes(0).Id.ToString() & """>" + objTypes(0).Name + "</a><span class='ms-webpartpagedescription'>" & _
    objTypes(0).Description + "</span></li>")
    ShowChildren(objID)
    Response.Write("</ol></td></tr></table>")
    
%>

</asp:Content>

<script runat="server">
    
    Public Sub ShowChildren(ByVal objID As SPContentTypeId)
        
        Dim objSite As SPSite = SPControl.GetContextSite(Context)
        Dim objTypes As SPContentTypeCollection = objSite.RootWeb.ContentTypes
        
        Response.Write("<ol>")
        
        For Each objType As SPContentType In objTypes
            If objType.Parent.Id = objID AndAlso objType.Parent.Id <> objType.Id Then
                
                Response.Write("<li><a class='ms-topnav'" & _
                " href=""/_layouts/ManageContentType.aspx?ctype=" & _
                objType.Id.ToString & """ > " + objType.Name & _
                "</a><span class='ms-webpartpagedescription'>" & _
                objType.Description + "</span></li>")
                
                ShowChildren(objType.Id)

                Response.Write("</ol>")
                
            End If
        Next
       
        
    End Sub

</script>