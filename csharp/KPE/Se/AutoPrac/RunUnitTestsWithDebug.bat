cls
cd bin\debug

REM https://github.com/nunit/docs/wiki/Console-Command-Line

nunit3-console.exe KPE.Se.AutoPrac.dll --trace=Debug --labels=On --workers=1 include=homepage

cd ..\..
