using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
class FileServer
{
    static void Main()

    {
        var server = new TcpListener(IPAddress.Any, 5000);
            server.Start();
        Console.WriteLine("Serever ready..........");
        while (true)
        {
            using var client = server.AcceptTcpClient();
            using var ns = client.GetStream();
            byte[] nameBuf = new byte[1024];
            int len = ns.Read(nameBuf);
            string file = Encoding.UTF8.GetString(nameBuf, 0, len);
            if (File.Exists(file)) {
                byte[] data = File.ReadAllBytes(file);
                ns.Write(data);
                Console.WriteLine($"SENT:{file}");
        }
            else {
                byte[] msg = Encoding.UTF8.GetBytes("FILE NOT FOUND");
                ns.Write(msg);
                Console.WriteLine($"Missing:{file}");
            } }
    }
}