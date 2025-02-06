using DomasticAidManagementSystem.Repositories.DBConfig.Services;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomasticAidManagementSystem.Repositories.DBConfig.Pricing
{
    public class PricingTableDBType
    {
        [Key]
        [Column("pricing_id")]
        public int PricingId { get; set; }

        [Column("service_id")]
        public int ServiceId { get; set; }

        [Column("additional_fee")]
        public decimal AdditionalFee { get; set; } = 0.00m;

        [Column("price_per_unit")]
        public decimal PricePerUnit { get; set; } = 0.00m;

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("ServiceId")]
        public virtual ServicesTableDBType Service { get; set; }
    }
}
