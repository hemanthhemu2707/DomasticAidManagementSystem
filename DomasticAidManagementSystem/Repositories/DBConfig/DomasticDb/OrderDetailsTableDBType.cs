using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomasticAidManagementSystem.Repositories.DBConfig.Order
{
    public class OrderDetailsTableDBType
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("OrderId")]
        public int OrderId { get; set; }

        [Required]
        [Column("SubCategoryId")]
        public int SubCategoryId { get; set; }

        [Required]
        [Column("Quantity")]
        public int Quantity { get; set; }

        [Required]
        [Column("UnitOfMeasureId")]
        public int UnitOfMeasureId { get; set; }

        [Column("totalcategoryamount", TypeName = "numeric(5,0)")]
        public decimal TotalCategoryAmount { get; set; }
    }
}
