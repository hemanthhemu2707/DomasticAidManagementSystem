using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomasticAidManagementSystem
{
    public class LoginDBTypes
    {
        [Key]
        [Column("us_id")]
        public int UserId { get; set; }

        [Column("us_name")]
        public string UserName { get; set; }

        [Column("us_pswd")]
        public string UserPassword { get; set; }

        [Column("us_phone_number")]
        public string UserPhone { get; set; }

        [Column("us_email_add")]
        public string UserEmailAdress { get; set; }

        [Column("us_is_admin")]
        public int AdminStatus { get; set; }

        [Column("us_approve_status")]
        public int UserApproveStatus { get; set; }


    }
}
