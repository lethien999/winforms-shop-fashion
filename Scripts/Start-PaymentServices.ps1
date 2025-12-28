# ============================================================
# Start-PaymentServices.ps1
# Script khởi động API Backend + Ngrok tự động
# Chỉ cần chạy 1 lần khi muốn test webhook
# ============================================================

param(
    [switch]$SkipNgrok,
    [int]$Port = 5000
)

$ErrorActionPreference = "Continue"
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = Split-Path -Parent $scriptPath

Write-Host "============================================" -ForegroundColor Cyan
Write-Host "  WinForms Fashion Shop - Payment Services  " -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host ""

# 1. Check if API is already running
Write-Host "[1/4] Checking if API is already running on port $Port..." -ForegroundColor Yellow
$portInUse = Get-NetTCPConnection -LocalPort $Port -ErrorAction SilentlyContinue
if ($portInUse) {
    Write-Host "  ✓ API already running on port $Port" -ForegroundColor Green
} else {
    Write-Host "  Starting API Backend..." -ForegroundColor Yellow
    $apiPath = Join-Path $projectRoot "API"
    Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$apiPath'; dotnet run" -WindowStyle Normal
    Write-Host "  ⏳ Waiting for API to start..." -ForegroundColor Yellow
    Start-Sleep -Seconds 5
    
    # Verify API started
    $portInUse = Get-NetTCPConnection -LocalPort $Port -ErrorAction SilentlyContinue
    if ($portInUse) {
        Write-Host "  ✓ API started successfully on port $Port" -ForegroundColor Green
    } else {
        Write-Host "  ❌ Failed to start API!" -ForegroundColor Red
    }
}

# 2. Start Ngrok (if not skipped)
if (-not $SkipNgrok) {
    Write-Host ""
    Write-Host "[2/4] Starting Ngrok tunnel..." -ForegroundColor Yellow
    
    # Refresh PATH to find ngrok
    $env:Path = [System.Environment]::GetEnvironmentVariable("Path","Machine") + ";" + [System.Environment]::GetEnvironmentVariable("Path","User")
    
    # Check if ngrok is installed
    $ngrokPath = Get-Command ngrok -ErrorAction SilentlyContinue
    if (-not $ngrokPath) {
        Write-Host "  ❌ Ngrok not installed! Install with: winget install ngrok.ngrok" -ForegroundColor Red
        Write-Host "  Then run: ngrok config add-authtoken YOUR_TOKEN" -ForegroundColor Yellow
    } else {
        # Kill existing ngrok processes
        Get-Process ngrok -ErrorAction SilentlyContinue | Stop-Process -Force -ErrorAction SilentlyContinue
        Start-Sleep -Seconds 1
        
        # Start ngrok
        Start-Process ngrok -ArgumentList "http", "$Port" -WindowStyle Normal
        Write-Host "  ⏳ Waiting for Ngrok to connect..." -ForegroundColor Yellow
        Start-Sleep -Seconds 3
        
        # Get ngrok URL from API
        try {
            $ngrokApi = Invoke-RestMethod -Uri "http://127.0.0.1:4040/api/tunnels" -TimeoutSec 5
            $publicUrl = $ngrokApi.tunnels[0].public_url
            
            Write-Host ""
            Write-Host "  ============================================" -ForegroundColor Green
            Write-Host "  ✓ NGROK TUNNEL ACTIVE" -ForegroundColor Green
            Write-Host "  ============================================" -ForegroundColor Green
            Write-Host "  Public URL: $publicUrl" -ForegroundColor White
            Write-Host ""
            Write-Host "  📋 WEBHOOK URL (copy to PayOS Dashboard):" -ForegroundColor Yellow
            Write-Host "  $publicUrl/api/payment/webhook" -ForegroundColor Cyan
            Write-Host "  ============================================" -ForegroundColor Green
            
            # Copy to clipboard
            "$publicUrl/api/payment/webhook" | Set-Clipboard
            Write-Host ""
            Write-Host "  ✓ Webhook URL copied to clipboard!" -ForegroundColor Green
        }
        catch {
            Write-Host "  ⚠️ Could not get Ngrok URL. Check the Ngrok window." -ForegroundColor Orange
        }
    }
} else {
    Write-Host ""
    Write-Host "[2/4] Skipping Ngrok (using direct PayOS polling)" -ForegroundColor Yellow
}

# 3. Test API
Write-Host ""
Write-Host "[3/4] Testing API connection..." -ForegroundColor Yellow
try {
    $response = Invoke-WebRequest -Uri "http://localhost:$Port/swagger" -UseBasicParsing -TimeoutSec 5 -ErrorAction SilentlyContinue
    Write-Host "  ✓ API responding at http://localhost:$Port" -ForegroundColor Green
}
catch {
    Write-Host "  ⚠️ API may not be ready yet. Wait a moment and try again." -ForegroundColor Orange
}

# 4. Summary
Write-Host ""
Write-Host "[4/4] Setup Complete!" -ForegroundColor Green
Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "  SERVICES STATUS" -ForegroundColor Cyan
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "  API Backend:  http://localhost:$Port" -ForegroundColor White
Write-Host "  Swagger UI:   http://localhost:$Port/swagger" -ForegroundColor White
if (-not $SkipNgrok) {
    Write-Host "  Ngrok Panel:  http://127.0.0.1:4040" -ForegroundColor White
}
Write-Host ""
Write-Host "  📌 NOTE: Hybrid Polling is ENABLED by default." -ForegroundColor Yellow
Write-Host "     App will poll PayOS directly every 5 seconds." -ForegroundColor Yellow
Write-Host "     Webhook is optional for real-time updates." -ForegroundColor Yellow
Write-Host ""
Write-Host "============================================" -ForegroundColor Cyan
Write-Host "Press any key to exit..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
