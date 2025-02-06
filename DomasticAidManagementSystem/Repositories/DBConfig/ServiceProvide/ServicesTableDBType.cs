using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomasticAidManagementSystem.Repositories.DBConfig.Services
{
    public class ServicesTableDBType
    {
        [Key]
        [Column("service_id")]
        public int ServiceId { get; set; }

        [Column("service_name")]
        public string ServiceName { get; set; }

        [Column("category")]
        public string Category { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("base_price")]
        public decimal BasePrice { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
