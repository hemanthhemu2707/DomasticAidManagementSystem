using DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem.Repositories.DBConfig
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryDbType>
    {
        private const string _tableName = "Category";
        private string _schemaName;

        public CategoryConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<CategoryDbType> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.CategoryId)
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.CategoryName)
                   .IsRequired()
                   .HasMaxLength(100);
        }
    }
}
