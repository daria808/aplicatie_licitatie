using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static async Task Main()
        {
            await AsyncServer.StartServerAsync();
        }
    }
}
