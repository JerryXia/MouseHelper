set RootDir=%~dp0..\MouseHelper
%WINDIR%\Microsoft.NET\Framework\v3.5\MSBuild.exe "%RootDir%\MouseHelper.csproj" /p:Configuration=Debug /t:ReBuild;Merge
pause
