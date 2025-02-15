using DomasticAidManagementSystem.Models.AdminMaster;
using Microsoft.AspNetCore.Mvc;

namespace DomasticAidManagementSystem
{
    public class AdminMasterController : Controller
    {
        private readonly IAdminMasterService adminMasterService;
        private readonly EmailService _emailService;

        public AdminMasterController(IAdminMasterService adminMasterService, EmailService emailService)
        {
            this.adminMasterService = adminMasterService;
            _emailService = emailService;
        }

        public async Task<IActionResult> AdminDashBoard(string UserId)
        {
            if (int.TryParse(UserId, out int numericUserId))
            {
                UserId = numericUserId.ToString();
            }
            else if (UserId != null)
            {
                UserId = UserId == "undefined" ? HttpContext.Session.GetString("UserId") : EncryptionHelper.UrlDecrypt(UserId);
            }
            else
            {
                UserId = HttpContext.Session.GetString("UserId");
            }
            var categoryCount = await adminMasterService.getCountDetails();
            return View(categoryCount);
        }


        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> AddCategory(string categoryName)
        {
            var category = await adminMasterService.AddMainCategory(categoryName);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam(string categoryName)
        {
            var category = await adminMasterService.AddTeam(categoryName);
            return Ok();
        }

        [HttpGet]
        public IActionResult AddEmployees()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var category = await adminMasterService.GetCategories();
            return Json(category);
        }

        [HttpGet]
        public async Task<IActionResult> LoadTeams()
        {
            var category = await adminMasterService.LoadTeams();
            return Json(category);
        }

        [HttpGet]
        public async Task<IActionResult> LoadEmployee()
        {
            var category = await adminMasterService.LoadEmployee();
            return Json(category);
        }

        [HttpGet]
        public async Task<IActionResult> AddUserCategory()
        {
            var category = await adminMasterService.GetCategories();
            return Json(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserCategory(string categoryName)
        {
            var category = await adminMasterService.AddMainCategory(categoryName);
            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUom()
        {
            var category = await adminMasterService.GetAllUom();
            return Json(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubCategory(int categoryId, string subCategoryName,int uomId, decimal BasePrice)
        {
            var category = await adminMasterService.AddSubCategory(categoryId, subCategoryName, uomId, BasePrice);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            var category = await adminMasterService.AddEmployee(employee);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSubCategory(int subCategoryId, int categoryId, string subCategoryName, int uomId, decimal BasePrice)
        {
            var category = await adminMasterService.UpdateSubCategory(subCategoryId, categoryId, subCategoryName, uomId, BasePrice);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetSubCategories()
        {
            var category = await adminMasterService.GetSubCategories(0);
            return Json(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployees(Employee employeeDetails)
        {
            var category = await adminMasterService.AddEmployee(employeeDetails);
            return Ok();
        }
    }
}
