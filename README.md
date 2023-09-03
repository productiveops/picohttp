

# PicoHTTP

PicoHTTP is a tiny web server written in .NET - less than 100 lines of code! The purpose of this
web server is to help with learning the HTTP protocol. You deal with raw HTTP requests and 
responses in this server allowing you to play around with the lower level HTTP details.

The server is not meant to be built and deployed anywhere. You simply run it in your IDE (Visual Studio, Rider or VS Code)
or on the command line.

## How to run

The code currently uses .NET 7 so ensure you have it  installed - <https://dotnet.microsoft.com/en-us/download/dotnet/7.0>

If you don't use an IDE then you can run the below in your Terminal/Command Prompt/PowerShell.

    dotnet run
    
The server will start at <http://localhost:5000>. If you open this URL in the browser, you will get a message like  "Hello, world! The current time is 15:18:29 PM" (with the current time, of course).

## Request Processors

The above "Hello, world..." response code is implemented in `HelloWorldRequestProcessor` class. There is another example
request processor called `AddNumbersRequestProcessor`. Go to `Program.cs` and replace `HelloWorldRequestProcessor` on the
below line with `AddNumbersRequestProcessor`.

Change this:

    var requestProcessor = new HelloWorldRequestProcessor();

To this:
 
    var requestProcessor = new AddNumbersRequestProcessor();

Now run the code and open <http://localhost/add/10/20>. You should get the response 30.

The request processors implement `IRequestProcessor` interface. You can write your own request processors and use them in `Program.cs`.

## License

The MIT License