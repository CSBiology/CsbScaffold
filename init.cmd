@echo off
cls

cd .env 
.paket\paket.exe init
.paket\paket.exe install
if errorlevel 1 (
  exit /b %errorlevel%
)

cd..
.env\packages\FAKE\tools\FAKE.exe init.fsx

set /p DUMMY=Finished initial setup of CSBScaffold. Hit ENTER to continue...