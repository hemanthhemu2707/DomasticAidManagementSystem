using DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategoryDbType>
{
    private const string _tableName = "SubCategory";
    private readonly string _schemaName;

    public SubCategoryConfiguration(string schemaName) => _schemaName = schemaName;

    public void Configure(EntityTypeBuilder<SubCategoryDbType> builder)
    {
        builder.ToTable(_tableName, _schemaName);

        builder.HasKey(sc => sc.SubCategoryId);

        builder.Property(sc => sc.SubCategoryId)
               .ValueGeneratedOnAdd();

        builder.Property(sc => sc.SubCategoryName)
               .IsRequired()
               .HasMaxLength(100);

        // Ensure CategoryId and UnitOfMeasureId are properly mapped
        builder.Property(sc => sc.CategoryId)
               .IsRequired();

        builder.Property(sc => sc.UnitOfMeasureId) // Fix column name (was "UnitOfMeaseId")
               .IsRequired();
    }
}
