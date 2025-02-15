using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb
{
    public class CategoryMappingDbType
    {
        [Key]
        [Column("Id")]
        public int MappingId { get; set; }

        [Column("CategoryId")]
        public int CategoryId { get; set; }

        [ForeignKey("Id")]
        public virtual CategoryDbType Category { get; set; }

        [Column("SubCategoryId")]
        public int SubCategoryId { get; set; }

        [ForeignKey("Id")]
        public virtual SubCategoryDbType SubCategory { get; set; }

        [Column("UnitOfMeasureId")]
        public int UnitId { get; set; }

        [ForeignKey("Id")]
        public virtual UnitOfMeasureDbType UnitOfMeasure { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
