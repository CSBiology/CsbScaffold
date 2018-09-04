@echo off
cls

cd .env 
.paket\paket.exe init
.paket\paket.exe install
if errorlevel 1 (
  exit /b %errorlevel%
)

cd..

git checkout -b local


powershell -Command "(gc .gitignore) -replace '\/\*\ ', '' | Out-File -encoding ASCII .gitignore"
powershell -Command "(gc .gitignore) -replace '!.env', '.env/' | Out-File -encoding ASCII .gitignore"
powershell -Command "(gc .gitignore) -replace '.env/packages/', '' | Out-File -encoding ASCII .gitignore"
powershell -Command "(gc .gitignore) -replace '!init.cmd', '' | Out-File -encoding ASCII .gitignore"
git rm -r --cached .env
git commit --message=init