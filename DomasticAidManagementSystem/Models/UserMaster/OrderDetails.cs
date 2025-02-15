namespace DomasticAidManagementSystem
{
    public class OrderDetails
    {
        public int OrderNumber { get; set; }
        public decimal OrderAmount { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
