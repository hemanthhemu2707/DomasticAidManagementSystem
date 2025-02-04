using Microsoft.AspNetCore.Mvc;

namespace DomasticAidManagementSystem
{
    public class UserMasterController : Controller
    {

        private readonly IUserMasterService _userMasterService;
                public UserMasterController(IUserMasterService userMasterService)
        { 
            _userMasterService = userMasterService;

        }

        [HttpGet]
        public async Task<IActionResult> UserDashBoard(string UserId)
        {
            //if (int.TryParse(UserId, out int numericUserId))
            //{
            //    UserId = numericUserId.ToString();
            //}
            //else if (UserId != null)
            //{
            //    UserId = EncryptionHelper.UrlDecrypt(UserId);
            //}

            //else
            //{
            //    UserId = HttpContext.Session.GetString("UserId");
            //}
            //UserDashBoard obj = new UserDashBoard();
            //obj.UserID = Convert.ToInt32( UserId);
            //if (UserId != null)
            //{
            //    var res = await _userMasterService.GetDashboardDetails(obj.UserID);
            //    HttpContext.Session.SetString("UserName", res.UserName.ToString());
            //    HttpContext.Session.SetString("UserId", res.UserID.ToString());
            //    HttpContext.Session.SetString("IsAdmin", res.AdminStatus.ToString());
            //    return View(res);

            //}
            return View(new UserDashBoard());
        }

        [HttpGet]
        public async Task<IActionResult> AddExpence(string? expenseId)
        {
            string UserId = HttpContext.Session.GetString("UserId");
            if (expenseId!=null)
            {
                var model = await _userMasterService.GetExpenceDetailsEdit(Convert.ToInt32(UserId), Convert.ToInt32(expenseId));
                model.ExpenceMasterId = Convert.ToInt32( expenseId);
                return View(model);
            }
            else
            {
                var model = await _userMasterService.GetExpenceDetails(Convert.ToInt32(UserId));
                return View(model);
            }
      
        }

        [HttpGet]
        public async Task<IActionResult> UserProfile()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddExpence([FromBody] AddExpence model)
            {
            string UserId = HttpContext.Session.GetString("UserId");
            model.ExpenceDate = Convert.ToDateTime(model.ExpenceDate);
            model.UserId = Convert.ToInt32(UserId);
            var response = await _userMasterService.AddExpenceAsync(model);

            return Json(response);
        }



        public async Task<IActionResult> GetFamilyInfo()
        {

            int UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var FamilyDetails = await _userMasterService.GetFamilyInformation(UserId);
            
            return View(FamilyDetails);
        }


        public async Task<IActionResult> GetExpenceReportDetaila(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate == null && toDate == null)
            {
                fromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                toDate = DateTime.Now;
            }
            string UserId = HttpContext.Session.GetString("UserId");
            var transactionsDetails = await _userMasterService.GetExpenceReportDetails(Convert.ToInt32(UserId), fromDate ?? DateTime.MinValue, toDate ?? DateTime.MinValue);
            var model = new ReportModel
            {
                FromDate = fromDate ?? DateTime.MinValue,
                ToDate = toDate ?? DateTime.MinValue,
                Transactions = transactionsDetails.ToList(),
                
            };

            return View(model);
        }


        public async Task<IActionResult> DownloadLastBill(DateTime fromDate, DateTime toDate)
        {
            DownloadLastBill ob = new DownloadLastBill();
            int UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            string selectedMonth = fromDate.ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture);
            ob = await _userMasterService.GetMontlyBillDetails(fromDate, toDate, UserId);
            ob.FamilyName = HttpContext.Session.GetString("FamilyName");
            ob.BillMonth = selectedMonth;   
            return View(ob);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExpense(string id)
        {
            int UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            var response = await _userMasterService.DeleteExpense(UserId, Convert.ToInt32( id));
            return Json(response);

        }
    }
    
    public class HtmlContentRequest
    {
        public string HtmlContent { get; set; }
    }
}




