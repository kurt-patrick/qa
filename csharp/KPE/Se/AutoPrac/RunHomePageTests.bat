cls
cd bin\debug

REM https://github.com/nunit/docs/wiki/Console-Command-Line

nunit3-console.exe KPE.Se.AutoPrac.dll include=homepage --trace=Debug --labels=On --workers=3

cd ..\..
