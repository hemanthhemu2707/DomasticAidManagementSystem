using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb
{
    public class CategoryDbType
    {
        [Column("Id")]
        public int CategoryId { get; set; }

        [Column("Name")]
        public string CategoryName { get; set; }
    }
}
