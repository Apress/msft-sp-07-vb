@SET TEMPLATEDIR="c:\program files\common files\microsoft shared\web server extensions\12\Template"
@SET ADMINDIR="c:\program files\common files\microsoft shared\web server extensions\12\Template"
@SET STSADM="c:\program files\common files\microsoft shared\web server extensions\12\bin\stsadm"

ECHO "Read these notes before continuing..."
ECHO "1) This installation assumes default locations on a en-us platform. For other installations, edit this INSTALL.BAT file."
ECHO "2) Before installing this feature, you must change the connection string in the Page_Load method of the file ULSLogViewer.aspx."
ECHO "3) This batch file will reset IIS after installation."
PAUSE

ECHO "Installing the ULS Log Viewer Feature"

MKDIR %TEMPLATEDIR%\FEATURES\LogViewer
XCOPY Feature.xml %TEMPLATEDIR%\FEATURES\LogViewer
XCOPY Elements.xml %TEMPLATEDIR%\FEATURES\LogViewer
XCOPY ULSLogViewer.aspx  %TEMPLATEDIR%\Admin
%STSADM% -o installfeature -filename  LogViewer\Feature.xml
IISRESET

ECHO "Installation Complete"
PAUSE
