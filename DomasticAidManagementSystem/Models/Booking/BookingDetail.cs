namespace DomasticAidManagementSystem.Domain.Entities
{
    public class BookingDetail
    {
        public int BookingDetailId { get; set; }
        public int BookingId { get; set; }
        public int ServiceId { get; set; }
        public int Quantity { get; set; } // Example: Number of rooms, hours, etc.
        public decimal TotalPrice { get; set; }
    }
}
