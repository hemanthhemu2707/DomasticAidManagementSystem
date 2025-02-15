using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomasticAidManagementSystem.Repositories.DBConfig.Order
{
    public class OrdersTableDBType
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("OrderDate")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column("Status")]
        [MaxLength(50)]
        public string Status { get; set; } = "Pending";

        [Column("totalamount", TypeName = "numeric(5,0)")]
        public decimal TotalAmount { get; set; }
    }
}
