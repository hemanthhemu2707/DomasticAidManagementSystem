using DomasticAidManagementSystem.Models.Categories;
using System.Text.Json;

namespace DomasticAidManagementSystem
{
    public interface IUserMasterRepo
    {
        Task<CategoryViewModel> GetMainContent(int categoryId);
        Task<DashBoard> SaveOrderDetails(JsonElement order, int userID);
        Task<OrderMaster> GetOrderDetails(string userID);
        Task<OrderMaster> GetOrderDetailForBill(int orderId);
    }
}
