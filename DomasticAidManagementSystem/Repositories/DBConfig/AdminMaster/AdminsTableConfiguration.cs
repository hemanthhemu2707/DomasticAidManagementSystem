using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem.Repositories.DBConfig.Admin
{
    public class AdminsTableConfiguration : IEntityTypeConfiguration<AdminsTableDBType>
    {
        private const string _tableName = "Admins";
        private string _schemaName;

        public AdminsTableConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<AdminsTableDBType> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.AdminId);
        }
    }
}
    