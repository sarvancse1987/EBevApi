namespace EBev.Domain
{
    public class Person : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string ShortDescription { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
