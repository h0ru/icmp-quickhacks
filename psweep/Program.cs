using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("[>] Developed by H0ru, check more on https://github.com/h0ru/icmp-quickhacks\r\nUse: psweep <IP Address>");
            return;
        }

        string IPAddress = args[0];

        Console.WriteLine($"\n[>] Starting the scanning at: {GetIpBase(IPAddress)}...\n");

        await PingSweep(GetIpBase(IPAddress), 1, 255);

        Console.WriteLine("\nFinished!");
    }

    static string GetIpBase(string IPAddress)
    {
        string[] octets = IPAddress.Split('.');
        if (octets.Length == 4)
        {
            return $"{octets[0]}.{octets[1]}.{octets[2]}";
        }
        return IPAddress;
    }

    static async Task PingSweep(string IPBase, int StartRange, int endRange)
    {
        var tasks = new Task[endRange - StartRange + 1];

        for (int i = StartRange; i <= endRange; i++)
        {
            string targetIpAddress = $"{IPBase}.{i}";
            tasks[i - StartRange] = PingHost(targetIpAddress);
        }

        await Task.WhenAll(tasks);

        for (int i = 0; i < tasks.Length; i++)
        {
            string targetIpAddress = $"{IPBase}.{i + StartRange}";
            PingReply reply = (tasks[i] as Task<PingReply>).Result;

            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine($"[+] Host {targetIpAddress} Online");
            }
        }
    }

    static Task<PingReply> PingHost(string IPAddress)
    {
        var ping = new Ping();
        return ping.SendPingAsync(IPAddress, timeout: 1000);
    }
}
