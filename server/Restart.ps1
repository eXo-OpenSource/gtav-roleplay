$config_path = "config.json";

if (Test-Path $config_path) {
	$config = Get-Content $config_path | Out-String | ConvertFrom-Json
	if ($config.server_path -And $config.server_working_dir) {
		Write-Host -ForegroundColor Green "Server Path:" $config.server_path
		Write-Host -ForegroundColor Green "Working-Directory:" $config.server_working_dir

		$altvserver = Get-Process altv-server -ErrorAction SilentlyContinue
		if ($altvserver) {
			$altvserver | Stop-Process -Force
		}
		dotnet publish -c Release

		if(Test-Path -LiteralPath $config.server_path) {
			Start-Process -FilePath $config.server_path -WorkingDirectory $config.server_working_dir
		} else {
			Write-Error "the executable could not be found:" $($config.server_path)
		}
	} else {
		Write-Error -ForegroundColor Red "Invalid config file!"
		Write-Host -NoNewLine 'Dr�cke eine Taste um fort zu fahren!';
	}
}else {
	Write-Error -ForegroundColor Red "Missing config file!"
	Write-Host -NoNewLine 'Press any key to continue!';
}
