using System.ComponentModel.DataAnnotations.Schema;

namespace DomasticAidManagementSystem.Repositories.DBConfig.DomasticDb
{
    public class EmployeeDbType
    {
        [Column("Id")]
        public int EmpId { get; set; }

        [Column("TeamId")]
        public int EmpTeamId { get; set; }

        [Column("EmpName")]
        public string EmpName { get; set; }

        [Column("EmpPhoneNumber")]
        public string EmpPhoneNumber { get; set; }

        [Column("EmpAadhar")]
        public string EmpAadharNumber { get; set; }
    }
}
