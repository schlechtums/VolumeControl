$parent = [System.IO.DirectoryInfo]::new($PSScriptRoot).Parent.FullName;
$consoleExePath = [System.IO.Path]::Combine($parent, "VolumeControl.Service.exe")

Write-Host "Installing $consoleExePath as the service 'Schlechtums.VolumeControl'"

try{
    New-Service -Name Schlechtums.VolumeControl -BinaryPathName "$consoleExePath" -StartupType Automatic
    
    Write-Host "Service installed successfully"
    Write-Host "Starting service..."
    Start-Service -Name Schlechtums.VolumeControl
    Write-Host "Service Started"
}
catch [System.SystemException] {
    Write-Host $_
}