namespace StudentSystem.Models
{
    public class License
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ResourceId { get; set; }

        public Resource Resource { get; set; }
    }
}