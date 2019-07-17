@echo off
cls
cd .env

.paket\paket.exe init
.paket\paket.exe install

if errorlevel 1 (
  exit /b %errorlevel%
)

set /p DUMMY=Finished initial setup of CSBScaffold. Hit ENTER to continue...