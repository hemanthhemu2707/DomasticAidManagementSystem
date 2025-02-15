namespace DomasticAidManagementSystem.Models.UserMaster
{
    public class OrderRequest
    {
        public string Address { get; set; }
        public DateTime DeliveryDateTime { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
