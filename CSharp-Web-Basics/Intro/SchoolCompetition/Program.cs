
namespace SchoolCompetition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Program
    {
        public static void Main()
        {
            Dictionary<string, int> pointsDict = new Dictionary<string, int>();
            Dictionary<string, SortedSet<string>> categoryDict = new Dictionary<string, SortedSet<string>>();

            while (true)
            {
                var input = Console.ReadLine();
                if (input == "END")
                {
                    break;
                }

                var splited = input.Split(' ');
                var studentName = splited[0];
                var studentCategory = splited[1];
                var studentPoints = int.Parse(splited[2]);

                if (!pointsDict.ContainsKey(studentName))
                {
                    pointsDict.Add(studentName, 0);
                }

                if (!categoryDict.ContainsKey(studentName))
                {
                    categoryDict.Add(studentName, new SortedSet<string>());
                }

                pointsDict[studentName] += studentPoints;
                categoryDict[studentName].Add(studentCategory);
            }

            var ordered = pointsDict.OrderByDescending(p => p.Value).ThenBy(n => n.Key);

            foreach (var student in ordered)
            {
                var categories = categoryDict[student.Key];
                Console.WriteLine($"{student.Key} {student.Value} [{string.Join(", ", categories)}]");
            }
        }
    }
}
