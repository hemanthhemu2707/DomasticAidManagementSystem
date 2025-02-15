using DomasticAidManagementSystem.Models.AdminMaster;
using DomasticAidManagementSystem.Models.Categories;

namespace DomasticAidManagementSystem
{
    public interface IAdminMasterRepo
    {
        Task<DashBoard> getCountDetails();
        Task<DashBoard> AddMainCategory(string categoryName);
        Task<DashBoard> AddTeam(string teamName);
        Task<DashBoard> AddSubCategory(int categoryId, string subCategoryName, int uomId , decimal BasePrice);
        Task<IEnumerable<Team>> LoadTeams();
        Task<IEnumerable<Employee>> LoadEmployee();
        Task<DashBoard> AddCategoryMap(string categoryName);
        Task<DashBoard> UpdateSubCategory(int subCategoryId, int categoryId, string subCategoryName, int uomId, decimal BasePrice);
        Task<IEnumerable<Category>> GetCategories();
        Task<IEnumerable<UnitOfMeasure>> GetAllUom();
        Task<IEnumerable<SubCategory>> GetSubCategories(int categoryId);
        Task<DashBoard> AddEmployees(Employee employee);

    }
}
