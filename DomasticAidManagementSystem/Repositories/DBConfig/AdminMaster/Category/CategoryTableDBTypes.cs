using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomasticAidManagementSystem
{
    public class CategoryTableDBTypes
    {
        [Key]
        [Column("exp_id")]
        public int ExpCatID { get; set; }

        [Column("exp_name")]
        public string ExpCatName { get; set; }

    }
}
