using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb
{
    public class SubCategoryDbType
    {
        [Key]
        [Column("Id")]
        public int SubCategoryId { get; set; }

        [Column("Name")]
        [Required]
        public string SubCategoryName { get; set; }

        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [Column("UomId")]
        public int UnitOfMeasureId { get; set; }  

        [Column("base_price")]
        public decimal BasePrice { get; set; }  

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now; 
    }
}
