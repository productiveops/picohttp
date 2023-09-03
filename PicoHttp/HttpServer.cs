using System.Net.Sockets;
using System.Net;
using System.Text;

namespace PicoHttp
{
    public interface IRequestProcessor
    {
        Task<string> ProcessAsync(string requestString);
    }

    public class HttpServer
    {
        private readonly string _ipAddress;
        private readonly int _port;
        private IRequestProcessor _requestProcessor;
        private Socket _socket = null!;

        public HttpServer(string ipAddress, int port, IRequestProcessor requestProcessor)
        {
            _ipAddress = ipAddress;
            _port = port;
            _requestProcessor = requestProcessor;
        }

        public async Task StartAsync(string[] args)
        {
            IPHostEntry ipHostInfo = await Dns.GetHostEntryAsync(_ipAddress);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint ipEndPoint = new(ipAddress, _port);

            using Socket listener = new(
                ipEndPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            Console.WriteLine($"Starting server at http://{_ipAddress}:{_port}");

            listener.Bind(ipEndPoint);
            listener.Listen(100);

            while (true)
            {
                _socket = await listener.AcceptAsync();

                try
                {
                    var requestString = await ReadRequestAsync();
                    var responseString = await _requestProcessor.ProcessAsync(requestString);
                    await SendResponseAsync(responseString);
                }
                catch (Exception)
                {
                    await SendResponseAsync(
    @"HTTP/1.1 500 Internal Server Error
Content-Type: text/html; charset=utf-8

");
                }
            }
        }

        private async Task<string> ReadRequestAsync()
        {
            var buffer = new byte[4096];
            var requestBytes = await _socket.ReceiveAsync(buffer, SocketFlags.None);
            var requestString = Encoding.UTF8.GetString(buffer, 0, requestBytes);
            return requestString;
        }

        private async Task SendResponseAsync(string responseString)
        {
            var responseBytes = Encoding.UTF8.GetBytes(responseString);
            await _socket.SendAsync(responseBytes, 0);
        }
    }
}
