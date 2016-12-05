@echo off

if not exist "%~dp0\Thirdparty\ACT\Advanced Combat Tracker.exe" (
	echo Error: Please copy "Advanced Combat Tracker.exe" into "Thirdparty\ACT" directory.
	goto END
)


set DOTNET_PATH=%windir%\Microsoft.NET\Framework\v4.0.30319
if not exist %DOTNET_PATH% (
	echo Error: Couldn't find .NET Framework directory. To execute the build, .NET Framework 4.5.1 or higher must be installed.
	goto END
)


%DOTNET_PATH%\msbuild /t:Rebuild /p:Configuration=Debug /p:Platform=x86 /p:OutputPath="%~dp0\BuildX86" "%~dp0\OverlayPlugin.sln"
%DOTNET_PATH%\msbuild /t:Rebuild /p:Configuration=Debug /p:Platform=x64 /p:OutputPath="%~dp0\BuildX64" "%~dp0\OverlayPlugin.sln"


:END
