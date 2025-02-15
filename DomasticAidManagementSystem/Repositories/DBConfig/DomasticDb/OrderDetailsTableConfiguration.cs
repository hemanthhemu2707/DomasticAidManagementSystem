using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem.Repositories.DBConfig.Order
{
    public class OrderDetailsTableConfiguration : IEntityTypeConfiguration<OrderDetailsTableDBType>
    {
        private const string _tableName = "OrderDetails";
        private readonly string _schemaName;

        public OrderDetailsTableConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<OrderDetailsTableDBType> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.Id);

            builder.Property(b => b.TotalCategoryAmount)
                   .HasColumnType("numeric(5,0)");
        }
    }
}
