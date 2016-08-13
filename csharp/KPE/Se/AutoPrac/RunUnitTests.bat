cls
cd bin\debug

REM https://github.com/nunit/docs/wiki/Console-Command-Line
REM --trace=Debug

nunit3-console.exe KPE.Se.AutoPrac.dll --labels=On --workers=3

cd ..\..\..\packages\ReportUnit.1.5.0-beta1\tools

ReportUnit ..\..\..\KPE.Se.AutoPrac\bin\Debug\TestResult.xml output.html

start TestResult.html

cd ..\..\..\KPE.Se.AutoPrac
