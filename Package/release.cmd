"..\..\oqtane.framework\oqtane.package\nuget.exe" pack Syncfusion.Helpdesk.nuspec 
XCOPY "*.nupkg" "..\..\oqtane.framework\Oqtane.Server\wwwroot\Modules\" /Y
