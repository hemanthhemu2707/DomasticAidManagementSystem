using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem
{
    public class ExpenseMasterTableConfiguration : IEntityTypeConfiguration<ExpenseMasterTableDBTypes>
    {
        private const string _tableName = "tblexpencemaster";
        private string _schemaName;

        public ExpenseMasterTableConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<ExpenseMasterTableDBTypes> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.ExpenceID);
        }
    }
}
