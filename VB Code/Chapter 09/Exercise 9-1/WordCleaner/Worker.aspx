<%@ Page Language="VB" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral,PublicKeyToken=71e9bce111e9429c"%>
<%@ Assembly Name="WordCleaner, Version=1.0.0.0, Culture=neutral,PublicKeyToken=8c9fc716f38d08b2"%>
<%@ Import Namespace="WordCleaner" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="Microsoft.SharePoint"%>
<%@ Import Namespace="Microsoft.SharePoint.WebControls"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Word Cleaner Worker Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
     <%

         Try
             'Get top-level site
             Dim objSite As SPSite = SPControl.GetContextSite(Context)
             
             'Build the file paths
             Dim strSource As String = "http://" + objSite.HostName + Request.QueryString("Item")
             Dim strCache As String = System.Environment.GetFolderPath(Environment.SpecialFolder.InternetCache)
             Dim strDownPath As String = strCache + "\" + strSource.Substring(strSource.LastIndexOf("/") + 1)
             Dim strExtension As String = strSource.Substring(strSource.LastIndexOf(".") + 1)
             
             'Make sure this is a DOCX file
             If Not (strExtension.ToUpper = "DOCX") Then
                 Throw New Exception("Only DOCX files can be cleaned.")
             End If
             
             'Download file
             Dim objClient As System.Net.WebClient = New System.Net.WebClient
             objClient.Credentials = System.Net.CredentialCache.DefaultCredentials
             objClient.DownloadFile(strSource, strDownPath)
             
             'Sanitize document
             Dim objWorker As WordCleaner.Worker = New WordCleaner.Worker
             objWorker.Sanitize(strDownPath)
             
             'Upload file
             Dim objStream As FileStream = New FileStream(strDownPath, FileMode.Open, FileAccess.Read)
             Dim objReader As BinaryReader = New BinaryReader(objStream)
             Dim bytes As Byte() = objReader.ReadBytes(CType(objStream.Length, Integer))
             objReader.Close()
             objStream.Close()
             objClient.UploadData(strSource, "PUT", bytes)
             
             'Redirect back to initial page
             Response.Redirect(strSource.Substring( _
             0, strSource.LastIndexOf("/")) & _
             "/Forms/AllItems.aspx")
             
         Catch x As Exception
             Response.Write(x.Message & _
             "" & Microsoft.VisualBasic.Chr(10) & "")
         End Try
            %>
        </div>
    </form>
</body>
</html>
