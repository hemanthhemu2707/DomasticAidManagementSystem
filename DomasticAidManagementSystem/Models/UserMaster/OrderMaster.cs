namespace DomasticAidManagementSystem
{
    public class OrderMaster
    {
        public string UserID { get; set; }
        public List<OrderHistory> OrderHistory { get; set; } = new List<OrderHistory>();
        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>(); // ✅ Ensure it's never null
        public decimal TotalOrderAmount { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
