namespace StudentSystem.Models
{
    using System;
    using System.Collections.Generic;

    public class Course
    {
        public Course()
        {
            this.Students = new List<StudentCourse>();
            this.Resources = new List<Resource>();
            this.Homeworks = new List<Homework>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public List<StudentCourse> Students { get; set; }

        public List<Resource> Resources { get; set; }

        public List<Homework> Homeworks { get; set; }
    }
}