using System.ComponentModel.DataAnnotations.Schema;

namespace DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb
{
    public class TeamDbType
    {
        [Column("Id")]
        public int TeamId { get; set; }

        [Column("TeamName")]
        public string TeamName { get; set; }
    }
}
