@echo off
setlocal

for %%I in ("%~dp0..") do set "ROOT=%%~fI"

where py >nul 2>nul
if not errorlevel 1 (
  py -3 "%ROOT%\scripts\build_package.py"
  if errorlevel 1 exit /b 1
  exit /b 0
)

where python >nul 2>nul
if not errorlevel 1 (
  python "%ROOT%\scripts\build_package.py"
  if errorlevel 1 exit /b 1
  exit /b 0
)

echo Python 3 nao foi encontrado. Instale o Python 3 para gerar o pacote.
exit /b 1
