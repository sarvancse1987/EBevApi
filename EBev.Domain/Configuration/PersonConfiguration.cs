namespace EBev.Domain.Configuration
{
    public class PersonConfiguration : BaseEntityConfigurations<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);
            builder.HasKey(x => x.Id).HasName("PK_Person");
            builder.Property(x => x.Name).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ImageUrl).HasMaxLength(250).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(400).IsRequired();
            builder.Property(x => x.Description).HasColumnType("NVARCHAR(MAX)").IsRequired();
        }
    }
}