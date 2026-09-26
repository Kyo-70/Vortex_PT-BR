@echo off
setlocal EnableExtensions

for %%I in ("%~dp0..") do set "ROOT=%%~fI"
set "DEFAULT_LOCALES=%LOCALAPPDATA%\Programs\Vortex\resources\locales"

if not exist "%DEFAULT_LOCALES%\en" set "DEFAULT_LOCALES=%ProgramFiles%\Black Tree Gaming Ltd\Vortex\resources\locales"

echo Informe a pasta resources\locales da instalacao do Vortex.
echo Deve existir uma pasta en dentro do caminho informado.
set /p "LOCALES=Destino [%DEFAULT_LOCALES%]: "
if "%LOCALES%"=="" set "LOCALES=%DEFAULT_LOCALES%"

if not exist "%LOCALES%\en" (
  echo Nao encontrei a pasta en em "%LOCALES%".
  echo Informe a pasta resources\locales correta e execute novamente.
  pause
  exit /b 1
)

if not exist "%ROOT%\resources\locales\pt-BR\info.json" (
  echo Os arquivos de idioma deste repositorio nao foram encontrados.
  pause
  exit /b 1
)

if not exist "%LOCALES%\pt-BR" mkdir "%LOCALES%\pt-BR"

for %%F in ("%ROOT%\resources\locales\pt-BR\*.json") do (
  if exist "%LOCALES%\pt-BR\%%~nxF" copy /Y "%LOCALES%\pt-BR\%%~nxF" "%LOCALES%\pt-BR\%%~nxF.ptbr.bak" >nul
  copy /Y "%%~fF" "%LOCALES%\pt-BR\%%~nxF" >nul
  if errorlevel 1 (
    echo Falha ao copiar %%~nxF.
    pause
    exit /b 1
  )
)

echo Arquivos de idioma copiados para "%LOCALES%\pt-BR".

choice /C SN /M "Aplicar tambem o patch experimental do Modlist Backup"
if errorlevel 2 goto finished

set /p "EXT_DIR=Pasta da extensao Modlist Backup: "
if not exist "%EXT_DIR%\index.js" (
  echo Nao encontrei index.js nesse caminho. O patch nao foi aplicado.
  goto finished
)

copy /Y "%EXT_DIR%\index.js" "%EXT_DIR%\index.js.ptbr.bak" >nul
if errorlevel 1 (
  echo Nao foi possivel criar a copia de seguranca do index.js.
  pause
  exit /b 1
)

copy /Y "%ROOT%\patches\modlist-backup\index.js" "%EXT_DIR%\index.js" >nul
if errorlevel 1 (
  echo Falha ao copiar o patch. Restaure index.js.ptbr.bak se necessario.
  pause
  exit /b 1
)

echo Patch do Modlist Backup aplicado. O original foi salvo como index.js.ptbr.bak.

:finished
echo Reinicie o Vortex para carregar as alteracoes.
pause
exit /b 0
