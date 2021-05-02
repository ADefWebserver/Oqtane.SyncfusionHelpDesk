"..\..\Oqtane\oqtane.package\nuget.exe" pack Syncfusion.Helpdesk.nuspec 
XCOPY "*.nupkg" "..\..\Oqtane\Oqtane.Server\wwwroot\Modules\" /Y
