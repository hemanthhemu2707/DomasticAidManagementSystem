using DomasticAidManagementSystem.Repositories.DBConfig.Bookings;
using DomasticAidManagementSystem.Repositories.DBConfig.Services;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DomasticAidManagementSystem.Repositories.DBConfig.BookingDetails
{
    public class BookingDetailsTableDBType
    {
        [Key]
        [Column("detail_id")]
        public int DetailId { get; set; }

        [Column("booking_id")]
        public int BookingId { get; set; }

        [Column("service_id")]
        public int ServiceId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price_per_unit")]
        public decimal PricePerUnit { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        [ForeignKey("BookingId")]
        public virtual BookingsTableDBType Booking { get; set; }

        [ForeignKey("ServiceId")]
        public virtual ServicesTableDBType Service { get; set; }
    }
}
