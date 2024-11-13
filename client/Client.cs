using System;
using System.Net.Sockets;
using System.Text;

public class Client
{
    private TcpClient client;
    private NetworkStream stream;

    public void StartClient()
    {
        try
        {
            client = new TcpClient("127.0.0.1", 12345);
            stream = client.GetStream();

            Console.WriteLine("Connected to server!");


            //aici am nevoie de functii 
            //
            // Instantiate the message sender and send a text message
            var messageSender = new MessageSender(stream);
            messageSender.SendTextMessage("Hello, server! I'm ready to communicate with you! :)");

            // Instantiate the image sender and send an image
            var imageSender = new ImageSender(stream);
            imageSender.SendImage(@"C:\Users\Daria\Desktop\p18.jpg");

            // Wait for a response from the server
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received from server: " + response);

            Console.ReadLine(); // Pause for testing

            client.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
