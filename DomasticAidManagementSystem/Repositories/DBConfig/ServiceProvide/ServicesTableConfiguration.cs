using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem.Repositories.DBConfig.Services
{
    public class ServicesTableConfiguration : IEntityTypeConfiguration<ServicesTableDBType>
    {
        private const string _tableName = "Services";
        private string _schemaName;

        public ServicesTableConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<ServicesTableDBType> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.ServiceId);
        }
    }
}
