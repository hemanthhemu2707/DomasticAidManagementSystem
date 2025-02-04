using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem
{
    public class LoginConfiguration : IEntityTypeConfiguration<LoginDBTypes>
    {
        private const string _tableName = "tbluser";
        private string _schemaName;

        public LoginConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<LoginDBTypes> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.UserId);
        }
    }
}
