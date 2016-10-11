<%@ Page Language="VB" Inherits="System.Web.UI.MobileControls.MobilePage" %>

<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0,Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Import Namespace="Microsoft.SharePoint.WebControls" %>
<%@ Import Namespace="System.Web.UI.MobileControls" %>
<%@ Register TagPrefix="mobile" Namespace="System.Web.UI.MobileControls" Assembly="System.Web.Mobile" %>

<script runat="server">
    
    Public Sub Page_Load()

        Dim site As SPSite = SPControl.GetContextSite(Context)
        Dim webs As SPWebCollection = site.AllWebs
 
        For Each web As SPWeb In webs
 
            Dim webLink As Link = New Link
            webLink.NavigateUrl = web.Url & _
             "/_layouts/mobile/default.aspx"
    
            webLink.Text = web.Title
            WelcomeForm.Controls.Add(webLink)
            
        Next
 
    End Sub
    
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<body>
    <mobile:Form ID="WelcomeForm" Runat="server">
        <mobile:Image ID="bannerImage" Runat="server" ImageUrl="../images/addtofavorites.gif">
        </mobile:Image>List of Sites<br />
    </mobile:Form>
</body>
</html>
