
namespace ValidateUrl
{
    using System;
    using System.Net;

    public class Program
    {
        public static void Main()
        {
            var input = Console.ReadLine();

            var encode = WebUtility.UrlDecode(input);
            
            var uri = new Uri(encode);

            Console.WriteLine($"Protocol: {uri.Scheme}");
            Console.WriteLine($"Host: {uri.Host}");
            Console.WriteLine($"Port: {uri.Port}");
            Console.WriteLine($"Path: {uri.AbsolutePath}");
            if (!string.IsNullOrEmpty(uri.Query))
            {
                Console.WriteLine($"Query: {uri.Query}");
            }

            if (!string.IsNullOrEmpty(uri.Fragment))
            {
                Console.WriteLine($"Fragment: {uri.Fragment}");
            }
        }
    }
}
