using DomasticAidManagementSystem.Repositories.DBConfig.Login;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomasticAidManagementSystem.Repositories.DBConfig.Admin
{
    public class AdminsTableDBType
    {
        [Key]
        [Column("admin_id")]
        public int AdminId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("role")]
        public string Role { get; set; } = "Manager";

        [ForeignKey("UserId")]
        public virtual UsersTableDBType User { get; set; }
    }
}
