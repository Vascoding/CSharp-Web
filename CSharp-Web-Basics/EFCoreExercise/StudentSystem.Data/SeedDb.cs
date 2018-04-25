using System;

namespace StudentSystem.Data
{
    using StudentSystem.Models;
    using StudentSystem.Models.Enumerations;

    public class SeedDb
    {
        public void SeedDatabase(StudentDbContext db)
        {
            Course dataBase = new Course
            {
                Name = "Database Basics",
                Description = "Learn MS SQL Server",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(4),
                Price = 330.00m
            };

            Course cSharpWeb = new Course
            {
                Name = "CSharp Web",
                Description = "Learn .NET Core and EF Core",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(5),
                Price = 330.00m,
            };

            Course programmingBasics = new Course
            {
                Name = "Programming Basics",
                Description = "Learn to code",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddMonths(2),
                Price = 10.00m
            };

            Student pesho = new Student
            {
                Name = "Pesho",
                RegisteredOn = DateTime.Now
            };
            

            Student gosho = new Student
            {
                Name = "Gosho",
                RegisteredOn = DateTime.Now
            };

            Student daniela = new Student
            {
                Name = "Daniela",
                RegisteredOn = DateTime.Now
            };

            StudentCourse studentCourseOne = new StudentCourse
            {
                Student = pesho,
                Course = programmingBasics
            };

            StudentCourse studentCourseTwo = new StudentCourse
            {
                Student = gosho,
                Course = cSharpWeb
            };
            
            StudentCourse studentCourseThree = new StudentCourse
            {
                Student = daniela,
                Course = dataBase
            };

            Resource book = new Resource
            {
                Name = "Programming Fundamentals With C#",
                Course = programmingBasics,
                ResourceType = ResourceType.Other,
                Url = "www.book.com"
            };

            Resource presentation = new Resource
            {
                Name = "Introduction to EF Core",
                Course = cSharpWeb,
                ResourceType = ResourceType.Presentation,
                Url = "www.softuni.bg"
            };

            Resource video = new Resource
            {
                Name = "Introduction to MS SQL Server",
                Course = dataBase,
                ResourceType = ResourceType.Video,
                Url = "www.ms-sql.bg"
            };

            Resource videoOne = new Resource
            {
                Name = "Crud",
                Course = dataBase,
                ResourceType = ResourceType.Video,
                Url = "www.ms-sql-Crud.bg"
            };

            Resource videoTwo = new Resource
            {
                Name = "Entity Framework",
                Course = dataBase,
                ResourceType = ResourceType.Video,
                Url = "www.ms-sql-EF.bg"
            };

            Resource videoThree = new Resource
            {
                Name = "Data Aggregations",
                Course = dataBase,
                ResourceType = ResourceType.Video,
                Url = "www.ms-sql-Data-Aggregations.bg"
            };

            Resource videoFour = new Resource
            {
                Name = "Joins",
                Course = dataBase,
                ResourceType = ResourceType.Video,
                Url = "www.ms-sql-Joins.bg"
            };

            Resource videoFive = new Resource
            {
                Name = "Triggers",
                Course = dataBase,
                ResourceType = ResourceType.Video,
                Url = "www.ms-sql-Triggers.bg"
            };

            License license1 = new License
            {
                Name = "License1",
                Resource = videoOne
            };

            License license2 = new License
            {
                Name = "License2",
                Resource = videoTwo
            };

            License license3 = new License
            {
                Name = "License3",
                Resource = videoThree
            };

            License license4 = new License
            {
                Name = "License4",
                Resource = videoFour
            };

            License license5 = new License
            {
                Name = "License5",
                Resource = videoFive
            };

            License license6 = new License
            {
                Name = "License6",
                Resource = video
            };

            License license7 = new License
            {
                Name = "License7",
                Resource = book
            };

            License license8 = new License
            {
                Name = "License8",
                Resource = presentation
            };

            Homework simpleCalc = new Homework
            {
                Student = pesho,
                Course = programmingBasics,
                Content = "Some content",
                ContentType = ContentType.Application,
                SubmissionDate = DateTime.Now
            };

            Homework web = new Homework
            {
                Student = gosho,
                Course = cSharpWeb,
                Content = "Some content",
                ContentType = ContentType.Zip,
                SubmissionDate = DateTime.Now
            };

            Homework dataB = new Homework
            {
                Student = daniela,
                Course = dataBase,
                Content = "Some content",
                ContentType = ContentType.Pdf,
                SubmissionDate = DateTime.Now
            };

            db.Courses.Add(programmingBasics);
            db.Courses.Add(cSharpWeb);
            db.Courses.Add(dataBase);

            db.Students.Add(pesho);
            db.Students.Add(gosho);
            db.Students.Add(daniela);

            db.StudentCourses.Add(studentCourseOne);
            db.StudentCourses.Add(studentCourseTwo);
            db.StudentCourses.Add(studentCourseThree);

            db.Homeworks.Add(simpleCalc);
            db.Homeworks.Add(web);
            db.Homeworks.Add(dataB);

            db.Resources.Add(book);
            db.Resources.Add(presentation);
            db.Resources.Add(video);
            db.Resources.Add(videoOne);
            db.Resources.Add(videoTwo);
            db.Resources.Add(videoThree);
            db.Resources.Add(videoFour);
            db.Resources.Add(videoFive);

            db.Licenses.Add(license1);
            db.Licenses.Add(license2);
            db.Licenses.Add(license3);
            db.Licenses.Add(license4);
            db.Licenses.Add(license5);
            db.Licenses.Add(license6);
            db.Licenses.Add(license7);
            db.Licenses.Add(license8);

            db.SaveChanges();
        }
    }
}
