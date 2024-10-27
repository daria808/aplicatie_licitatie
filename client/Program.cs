using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class Client
{
    public static void StartClient()
    {
        try
        {
            TcpClient client = new TcpClient("127.0.0.1", 12345);
            NetworkStream stream = client.GetStream();

            Console.WriteLine("Connected to server!");

            string message = "Hello, server!";
            byte[] data = Encoding.UTF8.GetBytes(message);
            stream.Write(data, 0, data.Length);
            Console.WriteLine("Sent: " + message);

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received from server: " + response);

            Console.ReadLine(); //am pus linia doar pentru test o sa o sterg mai incolo

            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}

class Program
{
    static void Main()
    {
        Client.StartClient();
    }
}
