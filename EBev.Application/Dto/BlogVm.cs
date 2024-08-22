namespace EBev.Application
{
    public class BlogVm: BaseVm
    {
        public string ThumbnailUrl { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int PersonId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
    }
}
