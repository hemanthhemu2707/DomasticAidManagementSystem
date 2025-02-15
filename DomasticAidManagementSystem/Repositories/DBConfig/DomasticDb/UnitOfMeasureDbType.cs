using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb
{
    public class UnitOfMeasureDbType
    {
        [Key]
        [Column("Id")]
        public int UnitId { get; set; }

        [Column("Name")]
        public string UnitName { get; set; } 
    }
}
