namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Student
    {
        public Student()
        {
            this.Courses = new List<StudentCourse>();
            this.Homeworks = new List<Homework>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int? Phonenumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? BirthDay { get; set; }

        public List<StudentCourse> Courses { get; set; }

        public List<Homework> Homeworks { get; set; }
    }
}