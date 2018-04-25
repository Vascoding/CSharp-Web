
namespace RequestParser
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main()
        {
            var requests = new Dictionary<string, HashSet<string>>();
            while (true)
            {
                var input = Console.ReadLine();
                if (input == "END")
                {
                    break;
                }
                var splitedRequest = input.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
                var path = $"/{splitedRequest[0]}";
                var method = splitedRequest[1];
                if (!requests.ContainsKey(path))
                {
                    requests.Add(path, new HashSet<string>());
                }
                requests[path].Add(method);
            }

            var inputResponse = Console.ReadLine();

            var splited = inputResponse.Split(new[] {' '});

            var requestMethod = splited[0].ToLower();
            var requestPath = splited[1];
            var requestProtocol = splited[2];

            var responseStatusNum = 404;
            var responseStatus = "Not Fount";
            
            if (requests.ContainsKey(requestPath) && requests[requestPath].Contains(requestMethod))
            {
                responseStatusNum = 200;
                responseStatus = "OK";
            }

            Console.WriteLine($"{requestProtocol} {responseStatusNum} {responseStatus}");
            Console.WriteLine($"Content-Length: {responseStatus.Length}");
            Console.WriteLine("Content-Type: text/plain");
            Console.WriteLine();
            Console.WriteLine($"{responseStatus}");

        }
    }
}
