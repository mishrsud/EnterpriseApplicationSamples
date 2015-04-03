param($installPath, $toolsPath, $package, $project)

if ($project) {
	Add-BindingRedirect -ProjectName $project.Name
	
	$newConfigFileSource = Join-Path $installPath "Files\App.config"
	$newConfigFileDestination = Join-Path $projectDestinationPath "app.config.new"

	$nlogConfigFileSource = Join-Path $installPath "Files\NLog.config"
	$nlogConfigFileDestination = Join-Path $projectDestinationPath "NLog.config.new"

	Copy-Item $newConfigFileSource $newConfigFileDestination -Force

	Copy-Item $nlogConfigFileSource $nlogConfigFileDestination -Force

	$project.DTE.ItemOperations.Navigate('https://github.com/mishrsud/EnterpriseApplicationSamples')
}