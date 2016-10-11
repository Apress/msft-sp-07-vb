@ECHO OFF
:: -----------------------------------------------
::
:: --------------------------
::  Post build actions (VS):
:: --------------------------
:: This batch file is run by VS after the user builds the project (F6), to automate 
:: installation/deployment of the workflow feature to SharePoint. 

:: By default, it skips all deployment steps so that users can build without deploying. 
:: Users can use the DEPLOY parameter to install the assembly the GAC and activate 
:: stsadm deployment commands.

:: Deployment steps differ depending on build configuration (Debug or Release).  
:: -Debug mode will copy files directly to the appropriate directories and activate the feature.
:: -Release mode will generate a WSP solution file (for production) and activate the feature
::  by installing the solution.

:: --------------------------
::  How to deploy your workflow to SharePoint:
:: --------------------------
::   1. Sign the assembly
::   2. Fill out the feature.xml and workflow.xml files (under DeploymentFiles->FeatureFiles)
::   3. Change "NODEPLOY" to "DEPLOY" in the post build events (in project properties, 
::      see "Compile"->"Build Events"->"Post-build event command line")
::   4. Press F6 or go to Build->(Re)Build Solution

:: --------------------------
::  Usage:
:: --------------------------
:: - Deployment disabled:  call "$(ProjectDir)\DeploymentFiles\PostBuildActions.bat" "$(ConfigurationName)" "$(ProjectDir)" "$(ProjectName)" "$(TargetDir)" "$(TargetName)" NODEPLOY
:: - Deployment  enabled:  call "$(ProjectDir)\DeploymentFiles\PostBuildActions.bat" "$(ConfigurationName)" "$(ProjectDir)" "$(ProjectName)" "$(TargetDir)" "$(TargetName)" DEPLOY
:: - Quick Deployment (install to GAC only):  call "$(ProjectDir)\DeploymentFiles\PostBuildActions.bat" "$(ConfigurationName)" "$(ProjectDir)" "$(ProjectName)" "$(TargetDir)" "$(TargetName)" DEPLOY QUICK

:: --------------------------
::  Notes:
:: --------------------------
:: - Results of the build are logged to VS output window (View->Output)

:: - Debug mode
::   1.  Use the QUICK parameter (see usage) to only reinstall the assembly to the GAC. 
::       (use if you have changed your workflow and have not changed your xml files or forms.)
::    
:: - Release mode
::   1.  Before deploying, add InfoPath form entries to wsp_structure.ddf and feature.xml files
::   2.  This batch file automatically replaces project and assembly names in the 
::       ProductionDeployment folder using SearchReplace.js. 
::   3.  If you encounter are errors, inspect the manifest.xml and wsp_structure.ddf files to 
::       make sure they were modified correctly
:: 
:: -----------------------------------------------

:: -----------------------------------------------
:: 

ECHO.
ECHO Running post build actions.
ECHO.

:: e.g."Debug" or "Release"
SET CONFIG=%1
SET CONFIG=%CONFIG:~1,-1%
ECHO CONFIG: %CONFIG%

:: e.g. "C:\Projects\MyWorkflowFeature"
SET PROJECTDIR=%2
SET PROJECTDIR=%PROJECTDIR:~1,-2%
ECHO PROJECTDIR: %PROJECTDIR%

:: e.g. "C:\Projects\MyWorkflowFeature\DeploymentFiles"
SET DEPLOYMENTDIR=%PROJECTDIR%\DeploymentFiles
ECHO DEPLOYMENTDIR: %DEPLOYMENTDIR%

:: e.g. "MyWorkflowFeature"
SET PROJECTNAME=%3
SET PROJECTNAME=%PROJECTNAME:~1,-1%
ECHO PROJECTNAME: %PROJECTNAME%

:: e.g. "C:\Projects\MyWorkflowFeature\bin\Debug"
SET TARGETDIR=%4
SET TARGETDIR=%TARGETDIR:~1,-2%
ECHO TARGETDIR: %TARGETDIR%

:: e.g. "MyWorkflowFeature"(.dll)
SET TARGETNAME=%5
SET TARGETNAME=%TARGETNAME:~1,-1%
ECHO TARGETNAME: %TARGETNAME%

:: cmd parameter, e.g. "DEPLOY" or "NODEPLOY"
SET DEPLOY=%6
ECHO DEPLOY: %DEPLOY%

:: constant placeholder for branch logic comparison; always "DEPLOY"
SET DEPLOYVALUE=DEPLOY
ECHO DEPLOYVALUE: %DEPLOYVALUE%

:: WSP manifest.xml and ddf file location, e.g. "C:\Projects\MyWorkflowFeature\DeploymentFiles\ProductionDeployment"
SET PRODUCTIONDIR=%DEPLOYMENTDIR%\ProductionDeployment
ECHO PRODUCTIONDIR: %PRODUCTIONDIR%

:: Feature.xml/workflow.xml location, e.g. "C:\Projects\MyWorkflowFeature\DeploymentFiles\FeatureFiles"
SET FEATUREDIR=%DEPLOYMENTDIR%\FeatureFiles
ECHO FEATUREDIR: %FEATUREDIR%

:: These values are set when a project name is changed to update the manifest.xml and wsp ddf file
SET OLDTARGETNAME=MyTarget
ECHO OLDTARGETNAME: %OLDTARGETNAME%

SET OLDPROJECTNAME=MyFeature
ECHO OLDPROJECTNAME: %OLDPROJECTNAME%

SET QUICK=false
IF "%7"=="QUICK" SET QUICK=true
ECHO QUICK: %QUICK%

IF NOT %DEPLOY%==%DEPLOYVALUE% (ECHO Skipping deployment & GOTO QUIT)

IF EXIST "%CommonProgramFiles%\Microsoft Shared\Web Server Extensions\12\BIN\STSADM.EXE" (SET STSADM="%CommonProgramFiles%\Microsoft Shared\Web Server Extensions\12\BIN\STSADM.EXE") ELSE (ECHO STSAMD.EXE could not be found! & GOTO QUIT)

IF %CONFIG%==Release GOTO RELEASE

:: 
:: -----------------------------------------------


:: -----------------------------------------------
:: 
:DEBUG
::
:: To customize this secion, find and replace
:: a) "%PROJECTNAME%" with your own feature name
:: b) "[IP_FORM_FILENAME]" with InfoPath form that needs to be uploaded (add additional lines for multiple forms)
:: c) "feature.xml" with the name of your feature.xml file
:: d) "workflow.xml" with the name of your workflow.xml file
:: e) "http://localhost" with the name of the site you wish to publish to

ECHO.
ECHO Using debug deployment
ECHO.

IF EXIST "%programfiles%\Microsoft Visual Studio 8\SDK\v2.0\Bin\gacutil.exe" (SET GACUTIL="%programfiles%\Microsoft Visual Studio 8\SDK\v2.0\Bin\gacutil.exe" & GOTO DEPLOY)
IF EXIST "%programfiles% (x86)\Microsoft Visual Studio 8\SDK\v2.0\Bin\gacutil.exe" (SET GACUTIL="%programfiles%\Microsoft Visual Studio 8\SDK\v2.0\Bin\gacutil.exe") ELSE (ECHO Gacutil.exe could not be found! & GOTO QUIT)


:DEPLOY

ECHO.
ECHO Adding assemblies to the GAC...
ECHO.

ECHO.
%GACUTIL% -uf %TARGETNAME%
ECHO.

ECHO.
%GACUTIL% -if "%TARGETDIR%\%TARGETNAME%.dll"
ECHO.

IF %QUICK%==true GOTO FINISH


ECHO.
ECHO Deploying the feature:
ECHO.


IF EXIST "%CommonProgramFiles%\Microsoft Shared\web server extensions\12\TEMPLATE\FEATURES" (SET FEATURESDIR=%CommonProgramFiles%\Microsoft Shared\web server extensions\12\TEMPLATE\FEATURES) ELSE (ECHO Features directory could not be found! & GOTO QUIT)

ECHO.
ECHO Copying the feature...
ECHO.

@ECHO ON

ECHO Feature Destination: "%FEATURESDIR%\%PROJECTNAME%"
ECHO.

:: e.g. "C:\Program Files\Common Files\Microsoft Shared\web server extensions\12\TEMPLATE\FEATURES\MyWorkflowFeature"
rd /s /q     "%FEATURESDIR%\%PROJECTNAME%"
mkdir        "%FEATURESDIR%\%PROJECTNAME%"

copy /Y      "%FEATUREDIR%\feature.xml"   "%FEATURESDIR%\%PROJECTNAME%"
copy /Y      "%FEATUREDIR%\workflow.xml"  "%FEATURESDIR%\%PROJECTNAME%"
xcopy /s /Y  "%FEATUREDIR%\*.xsn"         "%FEATURESDIR%\%PROJECTNAME%"

@ECHO OFF

ECHO.
ECHO Verifying InfoPath Forms...
ECHO.

ECHO No forms verified...
::Note: Verify InfoPath forms; copy line for each form
::%STSADM% -o verifyformtemplate -filename [IP_FORM_FILENAME]

ECHO.
ECHO Deactiviating and uninstalling the feature...
ECHO.

%STSADM% -o deactivatefeature -filename %PROJECTNAME%\feature.xml -url http://localhost
%STSADM% -o uninstallfeature  -filename %PROJECTNAME%\feature.xml

ECHO.
ECHO Activiating and installing the feature...
ECHO.

%STSADM% -o installfeature  -filename %PROJECTNAME%\feature.xml -force
%STSADM% -o activatefeature -filename %PROJECTNAME%\feature.xml -url http://localhost


GOTO FINISH

:: 
:: -----------------------------------------------


:: -----------------------------------------------
:: 
:RELEASE

ECHO.
ECHO Using ProductionDeployment
ECHO.

IF NOT EXIST "%VS80COMNTOOLS%\Microsoft.Office.Samples.ECM.SearchReplace.js" ECHO "%VS80COMNTOOLS%\Microsoft.Office.Samples.ECM.SearchReplace.js" not found! & GOTO QUIT

ECHO.
ECHO Updating manifest.xml and wsp_structure.ddf...
ECHO.

ECHO Generating new GUID for manifest.xml
"%VS80COMNTOOLS%\uuidgen.exe" > "%temp%\guid.tmp"
set /p GUID=<"%temp%\guid.tmp"
ECHO New GUID "%GUID%"

ECHO SearchReplace: "%PRODUCTIONDIR%\manifest.xml" "%OLDTARGETNAME%" "%TARGETNAME%" "%OLDPROJECTNAME%" "%PROJECTNAME%" "NEW_GUID" "%GUID%"
IF NOT "%OLDTARGETNAME%"=="%TARGETNAME%" (ECHO Performing replacement & CALL cscript "%VS80COMNTOOLS%\Microsoft.Office.Samples.ECM.SearchReplace.js" "%PRODUCTIONDIR%\manifest.xml" "%OLDTARGETNAME%" "%TARGETNAME%" "%OLDPROJECTNAME%" "%PROJECTNAME%" "NEW_GUID" "%GUID%") ELSE (ECHO Skipping replace)

ECHO SearchReplace: "%PRODUCTIONDIR%\wsp_structure.ddf" "%OLDTARGETNAME%" "%TARGETNAME%" "%OLDPROJECTNAME%" "%PROJECTNAME%" "ProductionPath" "%PRODUCTIONDIR%" "FeaturePath" "%FEATUREDIR%" "TargetPath" "%TARGETDIR%"
IF NOT "%OLDTARGETNAME%"=="%TARGETNAME%" (ECHO Performing replacement & CALL cscript "%VS80COMNTOOLS%\Microsoft.Office.Samples.ECM.SearchReplace.js" "%PRODUCTIONDIR%\wsp_structure.ddf"  "%OLDTARGETNAME%" "%TARGETNAME%" "%OLDPROJECTNAME%" "%PROJECTNAME%" "ProductionPath" "%PRODUCTIONDIR%" "FeaturePath" "%FEATUREDIR%" "TargetPath" "%TARGETDIR%") ELSE (ECHO Skipping replace)

ECHO.
ECHO Generating %PROJECTNAME%.wsp ...
ECHO.

makecab /f "%PRODUCTIONDIR%\wsp_structure.ddf"

ECHO.
ECHO Deploying the feature:
ECHO.

IF NOT %DEPLOY%==%DEPLOYVALUE% (ECHO Skipping deployment & GOTO FINISH)

ECHO.
ECHO Deactiviating and uninstalling the feature...
ECHO.

%STSADM% -o deactivatefeature  -filename %PROJECTNAME%\feature.xml  -url http://localhost/  -force
%STSADM% -o uninstallfeature   -filename %PROJECTNAME%\feature.xml  -force

ECHO.
ECHO Retracting and deleting solution (if it exists)...
ECHO.

%STSADM% -o retractsolution    -name %PROJECTNAME%.wsp              -local
%STSADM% -o deletesolution     -name %PROJECTNAME%.wsp

ECHO.
ECHO Adding and deploying the solution...
ECHO.

%STSADM% -o addsolution        -filename "%TARGETDIR%\Package\%PROJECTNAME%.wsp"
%STSADM% -o deploysolution     -name %PROJECTNAME%.wsp              -local                  -allowGacDeployment  -force

ECHO.
ECHO Activating and installing the feature...
ECHO.

%STSADM% -o installfeature     -filename %PROJECTNAME%\feature.xml  -force
%STSADM% -o activatefeature    -filename %PROJECTNAME%\feature.xml  -url http://localhost/  -force

GOTO FINISH

:: 
:: -----------------------------------------------


:: -----------------------------------------------
::
:FINISH
:: 

ECHO Doing an iisreset...
ECHO.

CALL iisreset

:: -----------------------------------------------



:: -----------------------------------------------
::
:QUIT

ECHO.
ECHO Done
ECHO.

:: 
:: -----------------------------------------------