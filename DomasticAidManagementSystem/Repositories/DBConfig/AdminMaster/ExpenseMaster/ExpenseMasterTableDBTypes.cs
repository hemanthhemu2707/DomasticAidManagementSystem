using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomasticAidManagementSystem
{
    public class ExpenseMasterTableDBTypes
    {
        [Key]
        [Column("e_exp_id")]
        public int ExpenceID { get; set; }

        [Column("exp_name")]
        public string ExpenceName { get; set; }

        [Column("e_price")]
        public double ExpensePrice { get; set; }

        [Column("e_catergory")]
        public string ExpenceCategory { get; set; }

        [Column("e_date")]
        public DateTime ExpenceDate { get; set; }

        [Column("e_us_id")]
        public int ExpenceUserId { get; set; }

        [Column("e_desc")]
        public string ExpenceDescription { get; set; }

        [Column("e_entry_date")]
        public DateTime ExpenceEntryDate { get; set; }

    }
}
