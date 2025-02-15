using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem.Repositories.DBConfig.Order
{
    public class OrdersTableConfiguration : IEntityTypeConfiguration<OrdersTableDBType>
    {
        private const string _tableName = "Orders";
        private readonly string _schemaName;

        public OrdersTableConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<OrdersTableDBType> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.Id);

            builder.Property(b => b.OrderDate)
                   .HasDefaultValueSql("GETDATE()");

            builder.Property(b => b.Status)
                   .HasMaxLength(50)
                   .HasDefaultValue("Pending");

            builder.Property(b => b.TotalAmount)
                   .HasColumnType("numeric(5,0)");
        }
    }
}
