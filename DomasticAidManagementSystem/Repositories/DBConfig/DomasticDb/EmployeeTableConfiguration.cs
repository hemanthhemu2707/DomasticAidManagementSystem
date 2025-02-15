using DomasticAidManagementSystem.Repositories.DBConfig.Order;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb
{
    public class EmployeeTableConfiguration : IEntityTypeConfiguration<EmployeeDbType>
    {
        private const string _tableName = "Employee";
        private readonly string _schemaName;

        public EmployeeTableConfiguration(string schemaName) => _schemaName = schemaName;

        public void Configure(EntityTypeBuilder<EmployeeDbType> builder)
        {
            builder.ToTable(_tableName, _schemaName);
            builder.HasKey(b => b.EmpId);
        }
    }
}