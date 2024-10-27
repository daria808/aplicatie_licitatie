using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class AsyncServer
{
    private static TcpListener listener;
    private static ConcurrentDictionary<TcpClient, Task> clients = new ConcurrentDictionary<TcpClient, Task>();
                        //o colectie thread safe pentru a pastra clientii conectati

    private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                        //aceasta este o instanta care va fi utilizata pentru anularea server ului

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

                //clientul este gestionat intr-o sarcina asincrona
                var clientTask = HandleClientAsync(client);
                clients.TryAdd(client, clientTask); //adaug clientul si sarcina in colectie

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

    private static async Task HandleClientAsync(TcpClient client)
    {
        using (client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024]; 

            int bytesRead;
            try
            {
                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                {
                    string dataReceived = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Received: " + dataReceived);

                    string response = "Server received: " + dataReceived;
                    byte[] responseData = Encoding.UTF8.GetBytes(response);
                    await stream.WriteAsync(responseData, 0, responseData.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while handling client: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Client disconnected.");
                clients.TryRemove(client, out _);
            }
        }
    }

    public static void StopServer()
    {
        cancellationTokenSource.Cancel();
        listener.Stop();
        Console.WriteLine("Server stopped.");
    }
}

class Program
{
    static async Task Main()
    {
        await AsyncServer.StartServerAsync();
    }
}
