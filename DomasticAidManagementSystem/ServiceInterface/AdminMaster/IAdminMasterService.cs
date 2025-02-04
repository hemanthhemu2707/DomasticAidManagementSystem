using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomasticAidManagementSystem
{
    public interface IAdminMasterService
    {

        public Task<DashBoard> GetDashboardCountAdminDash(int UserId);
        public Task<List<FamilyRequest>> GetFamilyDetails(int UserId);
        public Task<List<FamilyMemberDetails>> GetFamilyMembersDetails(int UserId);
        public Task<FamilyMemberDetails> SaveUpdateFamilyMemberDetails(FamilyMemberDetails familyMemberDetails);

        public Task<FamilyRequest> SaveUpdateFamilyDetails(FamilyRequest familyRequest);
        public Task<ExpenceCategory> SaveUpdateCategoryDetails(int categoryId, string categoryName, string categoryDescription);
        public Task<List<ExpenceCategory>> GetExpenseCategory();
        public Task<List<SelectListItem>> GetFamilyDetailsList();
        public Task<ExpenceCategory> DeleteExpenseCategory(int Category);

        public Task<bool> DeleteUserById(int UserId,int? status);

        public Task<bool> CheckIsEmailExist(string email);

    }
}
