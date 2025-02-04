using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem
{
    public class CategoryTableConfiguration : IEntityTypeConfiguration<CategoryTableDBTypes>
    {
        private const string _tableName = "tblexpencecategory";
        private string _schemaName;

        public CategoryTableConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<CategoryTableDBTypes> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.ExpCatID);
        }
    }
}
