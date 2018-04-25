
namespace EvenNumbersThread
{
    using System;
    using System.Threading;

    public class Program
    {
        public static void Main()
        {
            var start = int.Parse(Console.ReadLine());
            var end = int.Parse(Console.ReadLine());
            Thread thread = new Thread(() => PrintEvenNumbers(start, end));
            thread.Start();
            thread.Join();
            Console.WriteLine("Thread finished work");
        }

        private static void PrintEvenNumbers(int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine($"{i}");
                }
            }
        }
    }
}
