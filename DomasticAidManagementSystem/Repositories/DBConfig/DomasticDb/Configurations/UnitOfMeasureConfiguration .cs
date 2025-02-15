using DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem.Repositories.DBConfig
{
    public class UnitOfMeasureConfiguration : IEntityTypeConfiguration<UnitOfMeasureDbType>
    {
        private const string _tableName = "UnitOfMeasure";
        private string _schemaName;

        public UnitOfMeasureConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<UnitOfMeasureDbType> builder)
        {
            builder.ToTable(_tableName, _schemaName);

            builder.HasKey(u => u.UnitId);

            builder.Property(u => u.UnitId)
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.UnitName)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
