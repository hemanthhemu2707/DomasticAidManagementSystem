using DomasticAidManagementSystem.Repositories.DBConfig.Admin;
using DomasticAidManagementSystem.Repositories.DBConfig.BookingDetails;
using DomasticAidManagementSystem.Repositories.DBConfig.Bookings;
using DomasticAidManagementSystem.Repositories.DBConfig.Login;
using DomasticAidManagementSystem.Repositories.DBConfig.Pricing;
using DomasticAidManagementSystem.Repositories.DBConfig.Services;
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
            modelBuilder.ApplyConfiguration(new ServicesTableConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new PricingTableConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new BookingsTableConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new BookingDetailsTableConfiguration(schemaNme));
            modelBuilder.ApplyConfiguration(new AdminsTableConfiguration(schemaNme));
            base.OnModelCreating(modelBuilder);
		}
        public DbSet<UsersTableDBType> Users { get; set; }
        public DbSet<ServicesTableDBType> Services { get; set; }
        public DbSet<PricingTableDBType> Pricing { get; set; }
        public DbSet<BookingsTableDBType> Bookings { get; set; }
        public DbSet<BookingDetailsTableDBType> BookingDetails { get; set; }
        public DbSet<AdminsTableDBType> Admins { get; set; }
    }
}
