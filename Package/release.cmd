"..\..\oqtane.framework\oqtane.package\nuget.exe" pack Syncfusion.HelpDesk.nuspec 
XCOPY "*.nupkg" "..\..\oqtane.framework\Oqtane.Server\wwwroot\Modules\" /Y
