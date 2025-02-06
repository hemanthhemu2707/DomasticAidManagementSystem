using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem.Repositories.DBConfig.Login
{
    public class UserTableConfiguration : IEntityTypeConfiguration<UsersTableDBType>
    {
        private const string _tableName = "Users";  
        private string _schemaName;

        public UserTableConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<UsersTableDBType> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.UserId);
        }
    }
}
