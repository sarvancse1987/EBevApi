namespace EBev.Domain.Configuration
{
    public class BlogConfiguration : BaseEntityConfigurations<Blog>
    {
        public override void Configure(EntityTypeBuilder<Blog> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id).HasName("PK_Blog");
            builder.Property(x => x.ThumbnailUrl).HasMaxLength(150).IsRequired();
            builder.Property(x => x.Title).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Content).HasColumnType("NVARCHAR(MAX)").IsRequired();
        }
    }
}