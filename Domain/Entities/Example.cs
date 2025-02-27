namespace Domain.Entities
{
    public class Example
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Information { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
    }
}

