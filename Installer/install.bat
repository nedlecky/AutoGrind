@echo off
set filename=install.bat
set title=AutoGrind Installer
set version=2022-06-28
set description=Put latest development binaries, test recipes, and UR code into c:\AutoGrind

echo.
echo *** %title% ***   File: %filename%  Rev: %version%
echo =============================================================================================
echo Description: %description%
echo.

set AutoGrindRoot=C:\AutoGrind
echo This script puts the latest code in: %AutoGrindRoot%
echo.

set choice=n
set /p choice=Place latest in %AutoGrindRoot%? y/[n] 

if %choice%==y (
    rem bin and UR directories are mirrored to this source directory
    rem Recipes\Testing are only copied from here to root\Recipes\Testing if new files or newer dates than what is there
    robocopy AutoGrind\bin %AutoGrindRoot%\AutoGrind\bin /MIR
    robocopy Recipes\Testing %AutoGrindRoot%\Recipes\Testing /XO
    robocopy UR %AutoGrindRoot%\UR /MIR
    echo.
    echo Operation complete.
) else (
    echo.
    echo Operation Skipped.
)
