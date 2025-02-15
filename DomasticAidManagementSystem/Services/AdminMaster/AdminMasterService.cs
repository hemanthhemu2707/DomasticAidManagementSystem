using DomasticAidManagementSystem.Models.AdminMaster;
using DomasticAidManagementSystem.Models.Categories;

namespace DomasticAidManagementSystem
{
    public class AdminMasterService : IAdminMasterService
    {

        private readonly IAdminMasterRepo _adminMasterRepo;

        public AdminMasterService(IAdminMasterRepo adminMasterRepo)
        {
            _adminMasterRepo = adminMasterRepo;
        }

        public async Task<DashBoard> getCountDetails()
        {
            var details = await _adminMasterRepo.getCountDetails();
            return details;
        }

        public async Task<DashBoard> AddCategoryMap(string categoryName)
        {
            var details = await _adminMasterRepo.AddCategoryMap(categoryName);
            return details;
        }

        public async Task<DashBoard> AddMainCategory(string categoryName)
        {
            var details = await _adminMasterRepo.AddMainCategory(categoryName);
            return details;
        }

        public async Task<DashBoard> AddTeam(string teamName)
        {
            var details = await _adminMasterRepo.AddTeam(teamName);
            return details;
        }

        public async Task<DashBoard> AddSubCategory(int categoryId, string subCategoryName, int uomId, decimal BasePrice)
        {
            var details = await _adminMasterRepo.AddSubCategory(categoryId, subCategoryName, uomId, BasePrice);
            return details;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            var details = await _adminMasterRepo.GetCategories();
            return details;
        }

        public async Task<IEnumerable<Team>> LoadTeams()
        {
            var details = await _adminMasterRepo.LoadTeams();
            return details;
        }

        public async Task<IEnumerable<Employee>> LoadEmployee()
        {
            var details = await _adminMasterRepo.LoadEmployee();
            return details;
        }

        public async Task<IEnumerable<UnitOfMeasure>> GetAllUom()
        {
            var details = await _adminMasterRepo.GetAllUom();
            return details;
        }

        public async Task<IEnumerable<SubCategory>> GetSubCategories(int categoryId)
        {
          
                var details = await _adminMasterRepo.GetSubCategories(categoryId);
                return details;
     
         
        }

        public async Task<DashBoard> AddEmployee(Employee employee)
        {
            var details = await _adminMasterRepo.AddEmployees(employee);
            return details;
        }


        public async Task<DashBoard> UpdateSubCategory(int subCategoryId, int categoryId, string subCategoryName, int uomId, decimal BasePrice)
        {
            var details = await _adminMasterRepo.UpdateSubCategory(subCategoryId, categoryId, subCategoryName, uomId, BasePrice);
            return details;
        }

        public Task<DashBoard> AddEmployees(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
