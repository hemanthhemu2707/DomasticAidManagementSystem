using DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem.Repositories.DBConfig
{
    public class CategoryMappingConfiguration : IEntityTypeConfiguration<CategoryMappingDbType>
    {
        private const string _tableName = "CategoryMapping";
        private string _schemaName;

        public CategoryMappingConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<CategoryMappingDbType> builder)
        {
            builder.ToTable(_tableName, _schemaName);

            builder.HasKey(cm => cm.MappingId);

            builder.Property(cm => cm.MappingId)
                   .ValueGeneratedOnAdd();

            builder.Property(cm => cm.CreatedAt)
                   .HasDefaultValueSql("GETDATE()");

            // Many-to-Many: A Category can have multiple SubCategories, and each SubCategory belongs to one Category
            builder.HasOne(cm => cm.Category)
                   .WithMany()
                   .HasForeignKey(cm => cm.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cm => cm.SubCategory)
                   .WithMany()
                   .HasForeignKey(cm => cm.SubCategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(cm => cm.UnitOfMeasure)
                   .WithMany()
                   .HasForeignKey(cm => cm.UnitId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
