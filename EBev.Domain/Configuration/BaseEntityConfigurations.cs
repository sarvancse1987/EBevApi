namespace EBev.Domain
{
    public class BaseEntityConfigurations<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id).HasAnnotation("Order", 1);
            builder.Property(x => x.Id).UseIdentityColumn().IsRequired();
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.CreatedBy).IsRequired();
            builder.Property(x => x.CreatedOn).HasDefaultValueSql("GETDATE()");
            builder.Property(x => x.EditedBy);
            builder.Property(x => x.EditedOn);
        }
    }
}
