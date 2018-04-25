
namespace URLDecode
{
    using System;
    using System.Net;

    public class Program
    {
        public static void Main()
        {
            var input = Console.ReadLine();
            var decode = WebUtility.UrlDecode(input);
            Console.WriteLine(decode);
        }
    }
}
