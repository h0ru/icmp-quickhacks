function sweep {
    param(
        [string]$IPAddress
    )

    function Invoke-Parallel {
        param(
            [scriptblock]$ScriptBlock,
            [array]$ArgumentList
        )

        $runspaces = @()

        foreach ($arg in $ArgumentList) {
            $runspace = [runspacefactory]::CreateRunspace()
            $runspace.Open()
            $powershell = [powershell]::Create().AddScript($ScriptBlock).AddArgument($arg)
            $powershell.Runspace = $runspace
            $job = $powershell.BeginInvoke()
            $runspaces += [PSCustomObject]@{
                PowerShell = $powershell
                Job = $job
            }
        }

        $results = @()

        foreach ($runspace in $runspaces) {
            $runspace.PowerShell.EndInvoke($runspace.Job)
            $results += $runspace.PowerShell
        }

        return $results
    }

    Write-Host "[>] Developed by H0ru, check more on https://github.com/h0ru/icmp-quickhacks"
    Write-Host ""
    Write-Host "[>] Starting the scanning at: $IPAddress ..."

    $IPBase = $IPAddress -split '\.'
    $IPBase = $IPBase[0..2] -join '.'

    $tasks = @()

    for ($i = 1; $i -le 255; $i++) {
        $targetIpAddress = "$IPBase.$i"
        $ping = New-Object System.Net.NetworkInformation.Ping
        $tasks += $ping.SendPingAsync($targetIpAddress, 1000)
    }

    $results = Invoke-Parallel -ScriptBlock {
        param($task)
        $task.Wait()
        return $task.Result
    } -ArgumentList $tasks

    foreach ($result in $results) {
        if ($result.Status -eq "Success") {
            Write-Host "[+] Host $($result.Address) Online"
        }
    }

    Write-Host ""
    Write-Host "Finished!"
}

sweep $args[0]
