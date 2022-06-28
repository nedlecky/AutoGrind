@echo off
set filename=get_latest.bat
set title=AutoGrind Get Latest
set version=2022-06-28
set description=Pulls latest development binaries, recipes, and UR code into this directory

echo.
echo *** %title% ***   File: %filename%  Rev: %version%
echo =============================================================================================
echo Description: %description%
echo.

echo Your Registry Root is:
REG QUERY HKEY_CURRENT_USER\SOFTWARE\AutoGrind\ /v AutoGrindRoot

set AutoGrindRoot=C:\Users\nedlecky\GitHub\AutoGrind
echo This script pulls latest from : %AutoGrindRoot%
echo.

set choice=n
set /p choice=Get latest from %AutoGrindRoot%? y/[n] 

if %choice%==y (
    rmdir /s /q AutoGrind
    rmdir /s /q Recipes
    rmdir /s /q UR
    robocopy %AutoGrindRoot%\AutoGrind\bin AutoGrind\bin /MIR
    robocopy %AutoGrindRoot%\Recipes Recipes /MIR
    robocopy %AutoGrindRoot%\UR UR /MIR
    echo.
    echo Operation complete.
) else (
    echo.
    echo Operation Skipped.
)
