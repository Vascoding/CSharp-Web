namespace StudentSystem.Models
{
    using System.Collections.Generic;
    using StudentSystem.Models.Enumerations;

    public class Resource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ResourceType ResourceType { get; set; }

        public string Url { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public List<License> Licenses { get; set; }
    }
}