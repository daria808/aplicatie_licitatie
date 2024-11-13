using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        var client = new Client();
        client.StartClient();
    }
}
