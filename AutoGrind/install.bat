:: Installer
@echo off
echo AutoGrind Software Updater Rev 1.0
echo ================================================
echo %DATE%

set go=n
set /p go= Would you like to install? y/[n] 

echo 1) Install
echo 2) Backup
set /p choice= "Please Select one of the above options :" 

echo %go%
echo %choice%

set default=ABCD
set /p UserInputPath=%default%
echo %UserInputPath%