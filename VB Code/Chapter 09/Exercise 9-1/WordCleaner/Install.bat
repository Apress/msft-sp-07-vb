@SET FEATUREDIR="c:\program files\common files\microsoft shared\web server extensions\12\Template\Features"
@SET LAYOUTDIR="c:\program files\common files\microsoft shared\web server extensions\12\Template\Layouts"
@SET STSADM="c:\program files\common files\microsoft shared\web server extensions\12\bin\stsadm.exe"
@SET GACUTIL="C:\Program Files\Microsoft Visual Studio 8\SDK\v2.0\Bin\GacUtil.exe"

md %FEATUREDIR%\WordCleaner

xcopy /e /y Feature.xml %FEATUREDIR%\WordCleaner
xcopy /e /y Elements.xml %FEATUREDIR%\WordCleaner
xcopy /e /y Worker.aspx %LAYOUTDIR%

%GACUTIL% -if bin\debug\WordCleaner.dll

%STSADM% -o installfeature -filename  WordCleaner\feature.xml -force

IISRESET

ECHO Finished

PAUSE
