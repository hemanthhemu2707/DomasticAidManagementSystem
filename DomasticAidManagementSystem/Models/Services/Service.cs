namespace DomasticAidManagementSystem.Domain.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; } 
    }
}
