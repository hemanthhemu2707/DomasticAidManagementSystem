using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomasticAidManagementSystem
{
    public class AdminMasterService : IAdminMasterService
    {

        private readonly IAdminMasterRepo adminMasterRepo;

        public AdminMasterService(IAdminMasterRepo adminMasterRepo)
        {
            this.adminMasterRepo = adminMasterRepo;
        }

        public Task<bool> CheckIsEmailExist(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ExpenceCategory> DeleteExpenseCategory(int Category)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserById(int UserId, int? status)
        {
            throw new NotImplementedException();
        }

        public async Task<DashBoard> GetDashboardCountAdminDash()
        {
            var result = await adminMasterRepo.GetDashboardCountAdminDash();
                return result;
        }

        public Task<DashBoard> GetDashboardCountAdminDash(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<ExpenceCategory>> GetExpenseCategory()
        {
            throw new NotImplementedException();
        }

        public Task<List<FamilyRequest>> GetFamilyDetails(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<List<SelectListItem>> GetFamilyDetailsList()
        {
            throw new NotImplementedException();
        }

        public Task<List<FamilyMemberDetails>> GetFamilyMembersDetails(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<ExpenceCategory> SaveUpdateCategoryDetails(int categoryId, string categoryName, string categoryDescription)
        {
            throw new NotImplementedException();
        }

        public Task<FamilyRequest> SaveUpdateFamilyDetails(FamilyRequest familyRequest)
        {
            throw new NotImplementedException();
        }

        public Task<FamilyMemberDetails> SaveUpdateFamilyMemberDetails(FamilyMemberDetails familyMemberDetails)
        {
            throw new NotImplementedException();
        }
    }
}
