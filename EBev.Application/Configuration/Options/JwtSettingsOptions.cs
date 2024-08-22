namespace EBev.Application.Configuration.Options
{
    public class JwtSettingsOptions
    {
        public string Secret { get; set; }
        public TimeSpan TokenLifetime { get; set; }
    }
}
