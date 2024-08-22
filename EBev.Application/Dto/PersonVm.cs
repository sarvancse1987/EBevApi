namespace EBev.Application
{
    public class PersonVm : BaseVm
    {
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string ShortDescription { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
