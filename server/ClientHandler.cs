using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class ClientHandler
    {
        public static async Task HandleClientAsync(TcpClient client)
        {
            using (client)  //at the end client connection will close automatically
            {
                NetworkStream stream = client.GetStream();
                //stream associated with tcp, permits reading and writing betwenn server and client
                byte[] buffer = new byte[1024];

                try
                {
                    while (true)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        //read asincn data from client
                        if (bytesRead == 0) break;

                        string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        //convert brute data to string
                        await MessageProcessor.ProcessMessageAsync(dataReceived, stream);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error while handling client: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Client disconnected.");
                    AsyncServer.clients.TryRemove(client, out _);
                }
            }
        }

    }
}