namespace DomasticAidManagementSystem.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Confirmed, Cancelled
    }
}
