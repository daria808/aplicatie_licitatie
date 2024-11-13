using System;
using System.Net.Sockets;
using System.Text;

public class MessageSender
{
    private NetworkStream stream;

    public MessageSender(NetworkStream stream)
    {
        this.stream = stream;
    }

    public void SendTextMessage(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        stream.Write(data, 0, data.Length);
        Console.WriteLine("Sent: " + message);
    }
}
