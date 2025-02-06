using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem.Repositories.DBConfig.BookingDetails
{
    public class BookingDetailsTableConfiguration : IEntityTypeConfiguration<BookingDetailsTableDBType>
    {
        private const string _tableName = "BookingDetails";
        private string _schemaName;

        public BookingDetailsTableConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<BookingDetailsTableDBType> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.DetailId);
        }
    }
}
