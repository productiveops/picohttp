using PicoHttp;
using PicoHttp.RequestProcessors;

public partial class Program
{
    public static async Task Main(string[] args)
    {
        var ipAddress = "127.0.0.1";
        var port = 5000;

        var requestProcessor = new HelloWorldRequestProcessor();   // <--- Try another request processor from RequestProcessors directory
        //var requestProcessor = new AddNumbersRequestProcessor();

        var httpServer = new HttpServer(ipAddress, port, requestProcessor);
        await httpServer.StartAsync(args);
    }
}
