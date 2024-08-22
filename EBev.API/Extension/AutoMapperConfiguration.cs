namespace EBev.API.Extension
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration() : this("Profile")
        {

        }
        public AutoMapperConfiguration(string profileName) : base(profileName)
        {
            CreateMap<BlogVm, Blog>().ReverseMap();
            CreateMap<PersonVm, Person>().ReverseMap();
        }
    }
}