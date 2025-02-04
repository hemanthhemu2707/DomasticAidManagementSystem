using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem
{
    public class FamilyConfiguration : IEntityTypeConfiguration<FamilyTableDBTypes>
    {
        private const string _tableName = "tblfamily";
        private string _schemaName;

        public FamilyConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<FamilyTableDBTypes> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.FamilyID);
        }
    }
}
