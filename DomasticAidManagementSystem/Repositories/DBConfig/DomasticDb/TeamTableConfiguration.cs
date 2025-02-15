using DomasticAidManagementSystem.Repositories.DBConfig.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb
{
    public class TeamTableConfiguration : IEntityTypeConfiguration<TeamDbType>
    {
        private const string _tableName = "Team";
        private readonly string _schemaName;

        public TeamTableConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<TeamDbType> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.TeamId);
        }
    }
}