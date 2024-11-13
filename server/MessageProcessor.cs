using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class MessageProcessor
    {
        public static async Task ProcessMessageAsync(string dataReceived, NetworkStream stream)
        {
            if (dataReceived.StartsWith("UPLOAD"))
                await HandleImageUploadAsync(dataReceived, stream);
            else
                await HandleTextMessageAsync(dataReceived, stream);
        }

        private static async Task HandleImageUploadAsync(string dataReceived, NetworkStream stream)
        {
            string[] parts = dataReceived.Split(' ');
            if (parts.Length < 2 || !int.TryParse(parts[1], out int imageSize))
            {
                Console.WriteLine("Invalid UPLOAD command!!!");
                return;
            }

            byte[] imageData = new byte[imageSize];
            int totalBytesRead = 0;

            while (totalBytesRead < imageSize)
            {
                int read = await stream.ReadAsync(imageData, totalBytesRead, imageSize - totalBytesRead);
                totalBytesRead += read;
            }

            //folder path where i'm going to store my images
            string folderPath = @"C:\Users\Daria\Desktop\server_pics";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string imagePath = Path.Combine(folderPath, $"server_pics_{DateTime.Now:yyyyMMdd_HHmmss}.jpg");
            File.WriteAllBytes(imagePath, imageData);
            Console.WriteLine($"Image received and saved to {imagePath}");

            string response = "Image received successfully.";
            byte[] responseData = Encoding.UTF8.GetBytes(response);
            await stream.WriteAsync(responseData, 0, responseData.Length);

        }
        private static async Task HandleTextMessageAsync(string textReceived, NetworkStream stream)
        {
            Console.WriteLine("Received text message: " + textReceived);
            string response = "I've got the packet from the client! <3";
            byte[] responseData = Encoding.UTF8.GetBytes(response);
            await stream.WriteAsync(responseData, 0, responseData.Length);
        }
    }
}
