REM @echo off

:: The batch file can be used to trigger the report generation of StylecopViolation xml report. 
:: Location of stylecop generation report \obj\$configuration e.g. \obj\Debug, \obj\Release
:: Replace the argument to use custom configuration. Default build configuration used is "Debug"

ECHO Note: This Window will Automatically close once the report is generated.
ECHO ----
ECHO  
ECHO Generating stylecop results Report..........

CALL StylecopReportGen "C:\Workspaces\..\<YourSolutionFile>.sln" "Debug"

IF %ERRORLEVEL% NEQ 0  GOTO HOLD_WINDOW

:: START iexplore file://%CD%\StylecopViolations.html

EXIT

:HOLD_WINDOW
PAUSE