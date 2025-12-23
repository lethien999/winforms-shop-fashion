# Script để publish WinForms app với ClickOnce
# Usage: .\Scripts\Publish-ClickOnce.ps1 -Version "1.0.0" -PublishUrl "https://your-server.com/clickonce/"

param(
    [Parameter(Mandatory=$false)]
    [string]$Version = "1.0.0.0",
    
    [Parameter(Mandatory=$false)]
    [string]$PublishUrl = "",
    
    [Parameter(Mandatory=$false)]
    [string]$OutputPath = ".\publish\ClickOnce",
    
    [Parameter(Mandatory=$false)]
    [switch]$SelfContained = $true
)

Write-Host "=== Publishing WinForms App with ClickOnce ===" -ForegroundColor Cyan

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

# Update PublishUrl if provided
if ($PublishUrl -ne "") {
    $publishUrlPattern = '<PublishUrl>.*?</PublishUrl>'
    $newPublishUrl = "<PublishUrl>$PublishUrl</PublishUrl>"
    $csprojContent = $csprojContent -replace $publishUrlPattern, $newPublishUrl
}

Set-Content -Path $csprojPath -Value $csprojContent -NoNewline

Write-Host "Updated version to: $Version" -ForegroundColor Green
Write-Host "Output path: $OutputPath" -ForegroundColor Green

# Build publish command
$publishArgs = @(
    "publish",
    "GUI\GUI.csproj",
    "--configuration", "Release",
    "-p:PublishProfile=ClickOnce",
    "-p:PublishUrl=$OutputPath",
    "-p:ApplicationVersion=$Version"
)

if ($SelfContained) {
    $publishArgs += "--self-contained"
    $publishArgs += "true"
    $publishArgs += "--runtime"
    $publishArgs += "win-x64"
}

Write-Host "`nRunning: dotnet $($publishArgs -join ' ')" -ForegroundColor Yellow
& dotnet $publishArgs

if ($LASTEXITCODE -eq 0) {
    Write-Host "`n✅ ClickOnce publish completed successfully!" -ForegroundColor Green
    Write-Host "`nPublished files location: $OutputPath" -ForegroundColor Cyan
    Write-Host "`nTo deploy:" -ForegroundColor Yellow
    Write-Host "1. Copy all files from '$OutputPath' to your web server" -ForegroundColor White
    Write-Host "2. Ensure web server serves .application and .manifest files with correct MIME types" -ForegroundColor White
    Write-Host "3. Users can install by opening the .application file" -ForegroundColor White
} else {
    Write-Host "`n❌ ClickOnce publish failed!" -ForegroundColor Red
    exit 1
}

