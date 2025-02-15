namespace DomasticAidManagementSystem.Models.Categories
{
    public class SubCategory
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int CategoryId { get; set; }
        public decimal BasePrice { get; set; }
        public string MainCategoryName { get; set; }
        public string UnitOfMeasureName { get; set; }
    }
}
