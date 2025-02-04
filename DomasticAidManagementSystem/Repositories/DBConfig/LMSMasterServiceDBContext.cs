using DomasticAidManagementSystem;
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
            modelBuilder.ApplyConfiguration(new LoginConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new CategoryTableConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new ExpenseMasterTableConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new FamilyConfiguration(schemaNme));
            base.OnModelCreating(modelBuilder);
		}
        public DbSet<LoginDBTypes> LoginDetails { get; set; }
    }
}
