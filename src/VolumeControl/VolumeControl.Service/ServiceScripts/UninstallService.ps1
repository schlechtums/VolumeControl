Write-Host "Removing service 'Schlechtums.VolumeControl'"

try{
    Stop-Service -Name Schlechtums.VolumeControl
    Remove-Service Schlechtums.VolumeControl
    Write-Host "Service Schlectums.VolumeControl uninstalled"
}
catch [System.SystemException] {
    Write-Host $_
}