namespace DomasticAidManagementSystem.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int Role { get; set; } // 1 = Admin, 2 = Customer, etc.
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int Status {  get; set; }
        public string StatusMessage { get; set; }
        public string EncrptedUserId { get; set; }
        public int OTP { get; set; }
    }
}
