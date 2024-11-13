using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class AsyncServer
    {
        private static TcpListener listener;
        public static ConcurrentDictionary<TcpClient, Task> clients = new ConcurrentDictionary<TcpClient, Task>();
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public static async Task StartServerAsync()
        {
            listener = new TcpListener(IPAddress.Any, 12345);
            listener.Start();
            Console.WriteLine("Server started on port 12345...");

            try
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    TcpClient client = await listener.AcceptTcpClientAsync();
                    Console.WriteLine("Client connected!");

                    var clientTask = ClientHandler.HandleClientAsync(client);
                    clients.TryAdd(client, clientTask);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                StopServer();
            }
        }

        public static void StopServer()
        {
            cancellationTokenSource.Cancel();
            listener.Stop();
            Console.WriteLine("Server stopped.");
        }
    }
}
