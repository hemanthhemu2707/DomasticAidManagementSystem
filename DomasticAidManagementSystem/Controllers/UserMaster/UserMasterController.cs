using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Text.Json;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
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
            if (int.TryParse(UserId, out int numericUserId))
            {
                UserId = numericUserId.ToString();
            }
            else if (UserId != null)
            {
                UserId = UserId == "undefined" ? HttpContext.Session.GetString("UserId") : UserId;
            }
            else
            {
                UserId = HttpContext.Session.GetString("UserId");
            }

            // Temporary Data for Testing
            //var dashboardData = new UserDashBoard
            //{
            //    UserID = UserId,
            //    OrderHistory = new List<OrderHistory>
            //    {
            //        new OrderHistory { Date = "2024-02-08", Address = "123 Main St", Amount = 1500, Bill = "Bill123.pdf" },
            //        new OrderHistory { Date = "2024-02-07", Address = "456 Elm St", Amount = 1200, Bill = "Bill124.pdf" }
            //    },
            //    OrderDetails = new List<OrderDetails>
            //    {
            //        new OrderDetails { OrderNumber = 1001, OrderAmount = 2500, Quantity = 2, CategoryName = "Electronics" },
            //        new OrderDetails { OrderNumber = 1002, OrderAmount = 1800, Quantity = 1, CategoryName = "Furniture" }
            //    }
            //};

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> FinalSave(int bookingId)
        {

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetOrderDetails()
        {
            var orderMaster = await _userMasterService.GetOrderDetails(HttpContext.Session.GetString("UserId"));
            return View(orderMaster);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderHistory()
        {
            var orderMaster = await _userMasterService.GetOrderDetails(HttpContext.Session.GetString("UserId"));
            return View(orderMaster);
        }


        [HttpGet]
        public async Task<IActionResult> GetOrderDetailsBill()
        {
            var orderMaster = await _userMasterService.GetOrderDetails(HttpContext.Session.GetString("UserId"));
            return View(orderMaster);
        }


        public async Task<IActionResult> ViewBill(int orderId)
        {
            var order = await _userMasterService.GetOrderDetailForBill(orderId);

            if (order == null)
            {
                return NotFound("Order not found.");
            }

            return View(order);
        }

        public async Task<IActionResult> MainContent(int categoryId)
        {
            var response = await _userMasterService.GetMainContent(categoryId);
            return View(response);
        }

        [HttpGet]
        public IActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] JsonElement order)
        {
            int UserId = 0;
            if (HttpContext.Session.GetString("UserId")!=null)
            {
                UserId = Convert.ToInt32(HttpContext.Session.GetString("UserId"));
            }
            var details = await _userMasterService.SaveOrderDetails(order, UserId);
            return Json(details.Status);

        }

        public async Task<IActionResult> UserProfile()
        {
            return View();
        }




public async Task<IActionResult> GeneratePDF(int orderId)
    {


            var order = await _userMasterService.GetOrderDetailForBill(orderId);

        if (order == null)
        {
            return NotFound("Order not found.");
        }

        MemoryStream workStream = new MemoryStream();
        iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 20, 20, 20, 20);
        PdfWriter.GetInstance(doc, workStream).CloseStream = false;

        doc.Open();
        doc.Add(new Paragraph($"Order Invoice - #{order.OrderDetails.FirstOrDefault().OrderNumber}"));
        doc.Add(new Paragraph($"Date: {order.OrderDate}"));
        doc.Add(new Paragraph($"Total Amount: ${order.TotalOrderAmount}"));
        doc.Add(new Paragraph("\n"));

        PdfPTable table = new PdfPTable(3);
        table.AddCell("Item");
        table.AddCell("Quantity");
        table.AddCell("Price");

        foreach (var detail in order.OrderDetails)
        {
            table.AddCell(detail.SubCategoryName);
            table.AddCell(detail.Quantity.ToString());
            table.AddCell($"${detail.OrderAmount}");
        }

        doc.Add(table);
        doc.Close();

        byte[] byteStream = workStream.ToArray();
        return File(byteStream, "application/pdf", $"Invoice_{order.UserID}.pdf");
    }


}
}
