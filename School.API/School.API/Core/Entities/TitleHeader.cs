namespace School.API.Core.Entities
{
    public class TitleHeader
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[]? Photo { get; set; }
    }
}
