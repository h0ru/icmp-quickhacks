using System;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length != 1)
        {
            Console.WriteLine("[>] Developed by H0ru, check more on https://github.com/h0ru/icmp-quickhacks\r\nUse: psweepx <IP Address>");
            return;
        }

        string IPAddress = args[0];

        Console.WriteLine("\n[>] Starting the scanning at: " + GetIpBase(IPAddress) + "...\n");

        PingSweep(GetIpBase(IPAddress), 1, 255);

        Console.WriteLine("\nFinished!");
    }

    static string GetIpBase(string IPAddress)
    {
        string[] octets = IPAddress.Split('.');
        if (octets.Length == 4)
        {
            return string.Format("{0}.{1}.{2}", octets[0], octets[1], octets[2]);
        }
        return IPAddress;
    }

    static void PingSweep(string IPBase, int StartRange, int endRange)
    {
        var tasks = new Task[endRange - StartRange + 1];

        for (int i = StartRange; i <= endRange; i++)
        {
            string targetIpAddress = string.Format("{0}.{1}", IPBase, i);
            tasks[i - StartRange] = PingHost(targetIpAddress);
        }

        Task.WaitAll(tasks); // Wait for all tasks to complete

        for (int i = 0; i < tasks.Length; i++)
        {
            string targetIpAddress = string.Format("{0}.{1}", IPBase, i + StartRange);
            PingReply reply = (tasks[i] as Task<PingReply>).Result;

            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("[+] Host " + targetIpAddress + " Online");
            }
        }
    }

    static Task<PingReply> PingHost(string IPAddress)
    {
        var ping = new Ping();
        return ping.SendPingAsync(IPAddress, timeout: 1000);
    }
}
