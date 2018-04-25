
namespace StudentSystem
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Extensions.Internal;
    using StudentSystem.Data;
    using StudentSystem.Models;

    public class Program
    {
        public static void Main()
        {
            StudentDbContext db = new StudentDbContext();
            SeedDb seed = new SeedDb();
            using (db)
            {
                ClearDatabase(db);
                seed.SeedDatabase(db);
                // 2. Working with the Database --->

                // Task 1
                //ListStudentsAndHomeWorkSubmissions(db);

                // Task 2
                //ListAllCoursesWithResources(db);

                // Task 3
                //ListAllCoursesWithMoreThanFiveResourses(db);

                // Task 4
                //ListAllCoursesWhichAreActiveOnGivenDate(db);

                // Task 5
                //CalculateNumberOfCoursesForEachStudent(db);

                // 3. Resource Licenses --->

                // Task 1
                //ListAllCoursesWithTheyrCorrespondingResources(db);

                // Task 2
                //PrintStudentsAndCountOfCourses(db);

                StudentWithCourses(db);
            }
        }

        private static void StudentWithCourses(StudentDbContext db)
        {
            var students = db.Students
                .Select(s => new
                {
                    s.Name,
                    Coureses = s.Courses
                }).ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Student Name: {student.Name}");
                foreach (var item in student.Coureses)
                {
                    Console.WriteLine($"Course: {item.Course.Name}");
                }
            }

        }

        private static void PrintStudentsAndCountOfCourses(StudentDbContext db)
        {
            var students = db.Students
                .Select(c => new
                {
                    Name = c.Name,
                    CourcesCount = c.Courses.Count,
                    TotalResources = c.Courses.Sum(r => r.Course.Resources.Count),
                    TotalLicenses = c.Courses.Sum(a => a.Course.Resources.Sum(e => e.Licenses.Count))
                })
                .OrderByDescending(cn => cn.CourcesCount)
                .ThenByDescending(r => r.TotalResources)
                .ThenBy(e => e.Name).ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Student Name: {student.Name}");
                Console.WriteLine($"Courses Enrolled: {student.CourcesCount}");
                Console.WriteLine($"Total Resources: {student.TotalResources}");
                Console.WriteLine($"Total Licenses: {student.TotalLicenses}");
            }
        }

        private static void ListAllCoursesWithTheyrCorrespondingResources(StudentDbContext db)
        {
            var courses = db.Courses
                .Select(cr => new
                {
                    CourseName = cr.Name,
                    Resources = cr.Resources
                        .OrderByDescending(lc => lc.Licenses.Count)
                        .ThenBy(e => e.Name)
                        .Select(r => new
                        {
                            ResourceName = r.Name,
                            LicensesNames = r.Licenses.Select(n => n.Name),
                            LicenseCount = r.Licenses.Count
                        }),
                    ResourcesCount = cr.Resources.Count
                })
                .OrderByDescending(rc => rc.ResourcesCount)
                .ThenBy(cn => cn.CourseName)
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course Name: {course.CourseName}");
                
                foreach (var c in course.Resources)
                {
                    Console.WriteLine($"Resource Name: {c.ResourceName}");

                    foreach (var l in c.LicensesNames)
                    {
                        Console.WriteLine($"License Name: {l}");
                    }
                }
            }
        }

        private static void CalculateNumberOfCoursesForEachStudent(StudentDbContext db)
        {
            var students = db.Students
                .Select(sc => new
                {
                    Name = sc.Name,
                    NumberOfCourses = sc.Courses.Count,
                    TotalPrice = sc.Courses.Sum(c => c.Course.Price),
                    AveragePrice = sc.Courses.Average(c => c.Course.Price)
                })
                .OrderByDescending(t => t.TotalPrice)
                .ThenByDescending(a => a.NumberOfCourses)
                .ThenBy(n => n.Name)
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Student Name: {student.Name}");
                Console.WriteLine($"Number Of Courses: {student.NumberOfCourses}");
                Console.WriteLine($"Total Price: {student.TotalPrice:f2}");
                Console.WriteLine($"Average Price: {student.AveragePrice:f2}");
            }
        }

        private static void ListAllCoursesWhichAreActiveOnGivenDate(StudentDbContext db)
        {
            var date = DateTime.Now.AddMonths(3);
            var courses = db.Courses
                .Where(d => d.StartDate < date)
                .Where(e => e.EndDate > date)
                .Select(c => new
                {
                    CourseName = c.Name,
                    c.StartDate,
                    c.EndDate,
                    CourseDuration = c.EndDate.Subtract(c.StartDate).TotalDays,
                    StudentsCount = c.Students.Count
                })
                .OrderByDescending(sc => sc.StudentsCount)
                .ThenByDescending(d => d.CourseDuration)
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course Name: {course.CourseName}");
                Console.WriteLine($"Start Date: {course.StartDate}");
                Console.WriteLine($"End Date: {course.EndDate}");
                Console.WriteLine($"Course Duration: {course.CourseDuration:f2}");
                Console.WriteLine($"Students Count: {course.StudentsCount}");
            }

        }

        private static void ListAllCoursesWithMoreThanFiveResourses(StudentDbContext db)
        {
            var courses = db.Courses
                .Where(r => r.Resources.Count > 5)
                .OrderByDescending(rc => rc.Resources.Count)
                .ThenByDescending(sd => sd.StartDate)
                .Select(c => new
                {
                    Name = c.Name,
                    ResourceCount = c.Resources.Count
                }).ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course Name: {course.Name}{Environment.NewLine}ResourcesCount: {course.ResourceCount}");
            }
        }

        private static void ListAllCoursesWithResources(StudentDbContext db)
        {
            var courses = db.Courses
                .OrderBy(d => d.StartDate)
                .ThenByDescending(e => e.EndDate)
                .Select(c => new
                {
                    Name = c.Name,
                    Description = c.Description,
                    c.Resources
                }).ToList();

            foreach (var course in courses)
            {
                Console.WriteLine($"Course Name: {course.Name}{Environment.NewLine}Description: {course.Description}");
                foreach (var resource in course.Resources)
                {
                    Console.WriteLine($"Resource ID: {resource.Id}");
                    Console.WriteLine($"Resource Name: {resource.Name}");
                    Console.WriteLine($"Course ID: {resource.CourseId}");
                    Console.WriteLine($"Resource Name: {resource.ResourceType}");
                    Console.WriteLine($"Resource Name: {resource.Url}");
                }
            }
        }

        private static void ListStudentsAndHomeWorkSubmissions(StudentDbContext db)
        {
            var students = db.Students
                .Select(a => new
                {
                    Name = a.Name,
                    Homework = a.Homeworks
                        .Select(h => new
                        {
                            Content = h.Content,
                            ContentType = h.ContentType
                        })
                }).ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Name: {student.Name}");
                foreach (var stHomework in student.Homework)
                {
                    Console.WriteLine($"Content: {stHomework.Content}{Environment.NewLine}Content Type: {stHomework.ContentType}");
                }
            }
        }

        private static void ClearDatabase(StudentDbContext db)
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        }
    }
}
