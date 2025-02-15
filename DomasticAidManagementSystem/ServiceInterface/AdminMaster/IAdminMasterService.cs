using DomasticAidManagementSystem.Models.AdminMaster;
using DomasticAidManagementSystem.Models.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomasticAidManagementSystem
{
    public interface IAdminMasterService
    {
        Task<DashBoard> getCountDetails();
        Task<DashBoard> AddMainCategory(string categoryName);
        Task<DashBoard> AddTeam(string teamName);
        Task<DashBoard> AddSubCategory(int categoryId, string subCategoryName, int uomId, decimal BasePrice);
        Task<DashBoard> AddEmployee(Employee employee);
        Task<DashBoard> AddCategoryMap(string categoryName);
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<Team>> LoadTeams();
        Task<IEnumerable<Employee>> LoadEmployee();
        Task<IEnumerable<UnitOfMeasure>> GetAllUom();
        Task<DashBoard> UpdateSubCategory(int subCategoryId, int categoryId, string subCategoryName, int uomId, decimal BasePrice);
        Task<IEnumerable<SubCategory>> GetSubCategories(int categoryId);
    }
}
