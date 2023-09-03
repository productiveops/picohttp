namespace PicoHttp.RequestProcessors
{
    // Open http://localhost:5000
    // Hello, world! The current time is <CURRENT TIME HERE>
    public class HelloWorldRequestProcessor : IRequestProcessor
    {
        public async Task<string> ProcessAsync(string requestString)
        {
            Console.WriteLine($"Request: \n{requestString}");

            var message = $"<h1>Hello, world! The current time is {DateTime.Now.ToString("HH:mm:ss tt")}</h1>";
            var responseString =
    @$"HTTP 1.1 200 OK
Content-Type: text/html; charset=utf-8
Content-Length: {message.Length}

{message}
";

            Console.WriteLine($"Response: \n{responseString}");
            return responseString;
        }
    }
}
