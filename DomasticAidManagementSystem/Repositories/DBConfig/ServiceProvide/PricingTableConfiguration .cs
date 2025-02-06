using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DomasticAidManagementSystem.Repositories.DBConfig.Pricing
{
    public class PricingTableConfiguration : IEntityTypeConfiguration<PricingTableDBType>
    {
        private const string _tableName = "Pricing";
        private string _schemaName;

        public PricingTableConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<PricingTableDBType> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.PricingId);
        }
    }
}
