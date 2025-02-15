using DomasticAidManagementSystem.Repositories.DBConfig;
using DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb;
using DomasticAidManagementSystem.Repositories.DBConfig.Login;
using DomasticAidManagementSystem.Repositories.DBConfig.Order;
using Microsoft.EntityFrameworkCore;

namespace IIITS.EFCore.Repositories
{
    public class LMSMasterServiceDBContext : DbContext
	{
		private IConfiguration _configuration;
		private string _connectionString;
		private string _connectionStringNpg;
		private string _connectionStringWorkFlow;

		public LMSMasterServiceDBContext(IConfiguration config)
		{		
				_configuration=config ;
				//TODO
				_connectionString = _configuration["ConnectionStrings:SqlServerConnString"];
               _connectionStringNpg = _configuration["ConnectionStrings:PostgreSqlConnString"];
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            int commandTimeout = int.Parse(_configuration.GetSection("APIDBContext:CommandTimeout").Value);
            object value = optionsBuilder.UseSqlServer(_connectionString, options => options.CommandTimeout(commandTimeout))
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging(true);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			string schemaNme = _configuration["APIDBContext:SchemaName"];
            modelBuilder.ApplyConfiguration(new UserTableConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new CategoryConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new SubCategoryConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new UnitOfMeasureConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new OrderDetailsTableConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new OrdersTableConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new EmployeeTableConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new TeamTableConfiguration(schemaNme));
            base.OnModelCreating(modelBuilder);
		}
        public DbSet<UsersTableDBType> Users { get; set; }
        public DbSet<CategoryDbType> Categories { get; set; }
        public DbSet<SubCategoryDbType> SubCategories { get; set; }
        public DbSet<UnitOfMeasureDbType> UnitOfMeasures { get; set; }
        public DbSet<OrdersTableDBType> Orders { get; set; }
        public DbSet<OrderDetailsTableDBType> OrderDetails { get; set; }
        public DbSet<CategoryMappingDbType> CategoryMappings { get; set; }
        public DbSet<EmployeeDbType> Employees { get; set; }
        public DbSet<TeamDbType> Teams { get; set; }
    }
}
