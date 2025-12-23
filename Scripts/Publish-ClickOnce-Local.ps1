# Script để publish ClickOnce cho local deployment (file share hoặc local folder)
# Usage: .\Scripts\Publish-ClickOnce-Local.ps1 -Version "1.0.0"

param(
    [Parameter(Mandatory=$false)]
    [string]$Version = "1.0.0.0",
    
    [Parameter(Mandatory=$false)]
    [string]$OutputPath = ".\publish\ClickOnce-Local"
)

Write-Host "=== Publishing WinForms App with ClickOnce (Local) ===" -ForegroundColor Cyan

# Update version in .csproj
$csprojPath = ".\GUI\GUI.csproj"
$csprojContent = Get-Content $csprojPath -Raw

# Update ApplicationVersion
$versionPattern = '<ApplicationVersion>.*?</ApplicationVersion>'
$newVersion = "<ApplicationVersion>$Version</ApplicationVersion>"
$csprojContent = $csprojContent -replace $versionPattern, $newVersion

# Update ApplicationRevision (increment)
$revisionPattern = '<ApplicationRevision>(\d+)</ApplicationRevision>'
if ($csprojContent -match $revisionPattern) {
    $currentRevision = [int]$matches[1]
    $newRevision = $currentRevision + 1
    $csprojContent = $csprojContent -replace $revisionPattern, "<ApplicationRevision>$newRevision</ApplicationRevision>"
}

# Set InstallFrom to Disk for local deployment
$installFromPattern = '<InstallFrom>.*?</InstallFrom>'
$newInstallFrom = "<InstallFrom>Disk</InstallFrom>"
$csprojContent = $csprojContent -replace $installFromPattern, $newInstallFrom

# Update PublishUrl
$publishUrlPattern = '<PublishUrl>.*?</PublishUrl>'
$newPublishUrl = "<PublishUrl>$OutputPath</PublishUrl>"
$csprojContent = $csprojContent -replace $publishUrlPattern, $newPublishUrl

Set-Content -Path $csprojPath -Value $csprojContent -NoNewline

Write-Host "Updated version to: $Version" -ForegroundColor Green
Write-Host "Output path: $OutputPath" -ForegroundColor Green

# Create output directory
New-Item -ItemType Directory -Path $OutputPath -Force | Out-Null

# Publish using MSBuild with ClickOnce
$msbuildPath = "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
if (-not (Test-Path $msbuildPath)) {
    $msbuildPath = "C:\Program Files (x86)\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
}
if (-not (Test-Path $msbuildPath)) {
    Write-Host "MSBuild not found. Trying dotnet publish..." -ForegroundColor Yellow
    
    # Fallback to dotnet publish
    & dotnet publish GUI\GUI.csproj `
        --configuration Release `
        --output $OutputPath `
        --self-contained true `
        --runtime win-x64 `
        -p:ApplicationVersion=$Version
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "`n✅ Published successfully!" -ForegroundColor Green
        Write-Host "`nNote: For full ClickOnce features, use Visual Studio or install MSBuild" -ForegroundColor Yellow
    }
    exit $LASTEXITCODE
}

Write-Host "Using MSBuild: $msbuildPath" -ForegroundColor Cyan

# Build with ClickOnce publish
& $msbuildPath GUI\GUI.csproj `
    /t:Publish `
    /p:Configuration=Release `
    /p:PublishUrl=$OutputPath `
    /p:ApplicationVersion=$Version `
    /p:InstallFrom=Disk `
    /p:UpdateEnabled=true `
    /p:UpdateMode=Foreground `
    /p:SelfContained=true `
    /p:RuntimeIdentifier=win-x64

if ($LASTEXITCODE -eq 0) {
    Write-Host "`n✅ ClickOnce publish completed successfully!" -ForegroundColor Green
    Write-Host "`nPublished files location: $OutputPath" -ForegroundColor Cyan
    Write-Host "`nInstallation:" -ForegroundColor Yellow
    Write-Host "1. Share the folder '$OutputPath' on network or copy to USB" -ForegroundColor White
    Write-Host "2. Users can run 'setup.exe' or 'GUI.application' to install" -ForegroundColor White
    Write-Host "3. App will auto-update when new version is available" -ForegroundColor White
} else {
    Write-Host "`n❌ ClickOnce publish failed!" -ForegroundColor Red
    exit 1
}

