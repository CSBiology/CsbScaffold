@echo off
cls

.env\.paket\paket.exe init
.env\.paket\paket.exe install
if errorlevel 1 (
  exit /b %errorlevel%
)

set /p DUMMY=Finished initial setup of CSBScaffold. Hit ENTER to continue...