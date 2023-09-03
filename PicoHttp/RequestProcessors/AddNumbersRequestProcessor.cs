namespace PicoHttp.RequestProcessors
{
    // Open http://localhost:5000/add/10/20
    // It should display 
    public class AddNumbersRequestProcessor : IRequestProcessor
    {
        private string GetRequestLine(string requestString)
        {
            var lines = requestString.Split('\r');
            var requestLine = lines[0];
            return requestLine;
        }

        private (string method, string url) ParseRequestLine(string requestLine)
        {
            var parts = requestLine.Split(' ');
            return (parts[0], parts[1]);
        }

        public async Task<string> ProcessAsync(string requestString)
        {
            var requestLine = GetRequestLine(requestString);
            Console.WriteLine(requestLine);
            var (method, uri) = ParseRequestLine(requestLine);

            if (method == "GET")
            {
                if (uri.StartsWith("/add"))
                {
                    // We need the URI to be like /add/1/2
                    var parts = uri.Split('/');
                    if (parts.Length != 4)
                    {
                        return BadRequestResponse();
                    }

                    try
                    {
                        var firstNumber = int.Parse(parts[2]);
                        var secondNumber = int.Parse(parts[3]);
                        var sum = firstNumber + secondNumber;

                        return OkResponse(sum.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return InternalServerErrorResponse();
                    }
                }
                else
                {
                    return NotFoundResponse();
                }
            }

            return NotFoundResponse();
        }

        private string NotFoundResponse()
        {
            Console.WriteLine("404 Not Found");
            return
            @"HTTP/1.1 404 Not Found
Content-Type: text/html; charset=utf-8

";
        }

        private string BadRequestResponse()
        {
            Console.WriteLine("400 Bad Request");
            return
@"HTTP/1.1 400 Bad Request
Content-Type: text/html; charset=utf-8

";
        }

        private string InternalServerErrorResponse()
        {
            Console.WriteLine("500 Internal Server Error");
            return
@"HTTP/1.1 500 Internal Server Error
Content-Type: text/html; charset=utf-8

";
        }

        private string OkResponse(string message)
        {
            Console.WriteLine($"200 OK\n{message}");
            var response =
@$"HTTP/1.1 200 OK
Content-Type: text/html; charset=utf-8
Content-Length: {message.Length}

{message}
";
            return response;
        }
    }
}
