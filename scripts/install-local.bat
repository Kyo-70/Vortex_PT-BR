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
echo Reinicie o Vortex para carregar as alteracoes.
pause
exit /b 0
