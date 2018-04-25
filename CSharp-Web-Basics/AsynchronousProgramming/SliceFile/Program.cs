
namespace SliceFile
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main()
        {
            var sourcePath = Console.ReadLine();
            var destinationPath = Console.ReadLine();
            var parts = int.Parse(Console.ReadLine());

            SliceAsync(sourcePath, destinationPath, parts);

            Console.WriteLine("Anything eles?...");
            while (true)
            {
                Console.ReadLine();
            }
        }

        public static void Slice(string sourceFile, string destinationPath, int parts)
        {
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            using (var source = new FileStream(sourceFile, FileMode.Open))
            {
                FileInfo fileInfo = new FileInfo(sourceFile);

                var partLength = (source.Length / parts) + 1;
                long currentByte = 0;

                for (int currPart = 1; currPart <= parts; currPart++)
                {
                    string filePath = string.Format($"{destinationPath}/Part-{currPart}{fileInfo.Extension}");
                    using (var destination = new FileStream(filePath, FileMode.Create))
                    {
                        byte[] buffer = new byte[1024];

                        while (currentByte <= partLength * currPart)
                        {
                            int readBytesCount = source.Read(buffer, 0, buffer.Length);
                            if (readBytesCount == 0)
                            {
                                break;
                            }

                            destination.Write(buffer, 0, readBytesCount);
                            currentByte += readBytesCount;
                        }
                    }
                }
                Console.WriteLine("Slice complete.");
            }
        }

        static void SliceAsync(string sourceFile, string destinationPath, int parts)
        {
            Task.Run(() =>
            {
                Slice(sourceFile, destinationPath, parts);
            });
        }
    }
}
