using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

public class ImageSender
{
    private NetworkStream stream;

    public ImageSender(NetworkStream stream)
    {
        this.stream = stream;
    }

    public void SendImage(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("File not found: " + filePath);
            return;
        }

        // Read the image file
        byte[] imageData = File.ReadAllBytes(filePath);
        string uploadCommand = $"UPLOAD {imageData.Length}\n";
        byte[] commandData = Encoding.UTF8.GetBytes(uploadCommand);

        // Send the command and the image data
        stream.Write(commandData, 0, commandData.Length);
        stream.Write(imageData, 0, imageData.Length);
        Console.WriteLine("Image sent to client.");
    }
}