using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem.Repositories.DBConfig.Bookings
{
    public class BookingsTableConfiguration : IEntityTypeConfiguration<BookingsTableDBType>
    {
        private const string _tableName = "Bookings";
        private string _schemaName;

        public BookingsTableConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<BookingsTableDBType> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.BookingId);
        }
    }
}
