$altvserver = Get-Process altv-server -ErrorAction SilentlyContinue
if ($altvserver) {
    $altvserver | Stop-Process -Force
}
dotnet publish -c Release
Start-Process -FilePath "D:\AltV_Server\altv-server.exe" -WorkingDirectory "D:\AltV_Server\"
