using DomasticAidManagementSystem.Models.Categories;
using System.Text.Json;

namespace DomasticAidManagementSystem
{
    public class UserMasterService : IUserMasterService
    {
        private readonly IUserMasterRepo _userMasterRepo;

        public UserMasterService(IUserMasterRepo userMasterRepo)
        {
            _userMasterRepo = userMasterRepo ?? throw new ArgumentNullException(nameof(userMasterRepo));
        }
          
        public async Task<CategoryViewModel> GetMainContent(int categoryId)
        {
            var response = await _userMasterRepo.GetMainContent(categoryId);
            return response;
        }

        public async Task<DashBoard> SaveOrderDetails(JsonElement order, int userID)
        {
            var response = await _userMasterRepo.SaveOrderDetails(order, userID);
            return response;
        }

        public async Task<OrderMaster> GetOrderDetails( string userID)
        {
            var response = await _userMasterRepo.GetOrderDetails(userID);
            return response;
        }

        public async Task<OrderMaster> GetOrderDetailForBill(int orderId)
        {
            var response = await _userMasterRepo.GetOrderDetailForBill(orderId);
            return response;
        }

    }
}
