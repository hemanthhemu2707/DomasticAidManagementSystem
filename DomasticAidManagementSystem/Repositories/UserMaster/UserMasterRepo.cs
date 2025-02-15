using DomasticAidManagementSystem.Models.Categories;
using DomasticAidManagementSystem.Models.UserMaster;
using DomasticAidManagementSystem.Repositories.DBConfig.Order;
using IIITS.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DomasticAidManagementSystem
{
    public class UserMasterRepo : IUserMasterRepo
    {
        private readonly EmailService _emailService;

        private readonly IConfiguration _configuration;

        private readonly LMSMasterServiceDBContext _dbContext;

        public UserMasterRepo(LMSMasterServiceDBContext dbContext, IConfiguration configuration, EmailService emailService)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _emailService = emailService;
        }

        public async Task<CategoryViewModel> GetMainContent(int categoryId)
        {
            var category = await _dbContext.Categories
                .Where(c => c.CategoryId == categoryId)
                .FirstOrDefaultAsync();

            var subCategories = await _dbContext.SubCategories
                .Where(sc => sc.CategoryId == categoryId)
                .ToListAsync();

            var viewModel = new CategoryViewModel
            {
                CategoryName = category?.CategoryName,
                SubCategories = new List<SubCategory>()  // Initialize the list before adding items
            };

            foreach (var subCategory in subCategories)
            {
                viewModel.SubCategories.Add(new SubCategory
                {
                    BasePrice = subCategory.BasePrice,
                    CategoryId = subCategory.CategoryId,
                    MainCategoryName = category?.CategoryName, 
                    SubCategoryName = subCategory.SubCategoryName,
                    SubCategoryId = subCategory.SubCategoryId 
                });
            }

            return viewModel;
        }
        public async Task<DashBoard> SaveOrderDetails(JsonElement orderJson, int userID)
        {
            try
            {
                string fixedJson = orderJson.GetRawText();
                var order = JsonSerializer.Deserialize<OrderRequest>(orderJson.GetRawText(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (order == null || order.Items == null || !order.Items.Any())
                {
                    return new DashBoard { Status = -1 };
                }

                var newOrder = new OrdersTableDBType
                {
                    OrderDate = order.DeliveryDateTime,
                    TotalAmount = order.Items.Sum(x => x.Price * x.Qty),
                    UserId = userID,
                };

                _dbContext.Orders.Add(newOrder);
                await _dbContext.SaveChangesAsync();

                List<OrderDetailsTableDBType> orderDetailsList = new();

                foreach (var item in order.Items)
                {
                    orderDetailsList.Add(new OrderDetailsTableDBType
                    {
                        OrderId = newOrder.Id,
                        Quantity = item.Qty,
                        SubCategoryId = Convert.ToInt32(item.Id),
                        TotalCategoryAmount = item.Price * item.Qty,
                    });
                }

                await _dbContext.OrderDetails.AddRangeAsync(orderDetailsList);
                await _dbContext.SaveChangesAsync();

                // Generate and send order confirmation email
                await SendOrderConfirmationEmail(order, userID);

                return new DashBoard { Status = 1 };
            }
            catch (Exception ex)
            {
                return new DashBoard { Status = -1 };
            }
        }


        private async Task SendOrderConfirmationEmail(OrderRequest order, int userID)
        {
            // Get user details from DB
            var user = await _dbContext.Users.FindAsync(userID);
            if (user == null) return;

            // Generate random cleaning team name and employee
            string[] teamNames = { "Sparkle Crew", "Shiny Homes", "Eco Cleaners", "Swift Cleaning" };
            string[] employeeNames = { "John Smith", "Alice Johnson", "Michael Brown", "Emma Davis" };
            Random rnd = new Random();
            string teamName = teamNames[rnd.Next(teamNames.Length)];
            string employeeName = employeeNames[rnd.Next(employeeNames.Length)];

            // Generate order details as HTML
            string orderItemsHtml = "";
            foreach (var item in order.Items)
            {
                orderItemsHtml += $"<tr><td>{item.Name}</td><td>{item.Qty}</td><td>₹{item.Price}</td><td>₹{item.Price * item.Qty}</td></tr>";
            }

            // Email body with animation
            string userEmailBody = $@"
        <html>
        <head>
            <style>
                @keyframes fadeIn {{
                    from {{ opacity: 0; }} 
                    to {{ opacity: 1; }}
                }}

                .fade-in {{
                    animation: fadeIn 1.5s ease-in-out;
                }}

                .progress-bar {{
                    width: 100%;
                    background-color: #e0e0e0;
                    border-radius: 5px;
                    overflow: hidden;
                }}

                .progress-bar div {{
                    width: 100%;
                    height: 10px;
                    background-color: #4CAF50;
                    animation: fillProgress 2s ease-in-out forwards;
                }}

                @keyframes fillProgress {{
                    from {{ width: 0; }} 
                    to {{ width: 100%; }}
                }}

                .container {{
                    font-family: Arial, sans-serif;
                    max-width: 600px;
                    margin: auto;
                    border: 1px solid #ddd;
                    padding: 20px;
                    border-radius: 10px;
                    box-shadow: 2px 2px 10px rgba(0,0,0,0.1);
                }}

                .header {{
                    text-align: center;
                    font-size: 22px;
                    font-weight: bold;
                    color: #333;
                }}

                .order-summary {{
                    border-collapse: collapse;
                    width: 100%;
                    margin-top: 20px;
                }}

                .order-summary th, .order-summary td {{
                    border: 1px solid #ddd;
                    padding: 10px;
                    text-align: left;
                }}

                .order-summary th {{
                    background-color: #4CAF50;
                    color: white;
                }}

                .footer {{
                    text-align: center;
                    margin-top: 20px;
                    font-size: 14px;
                    color: #666;
                }}

            </style>
        </head>
        <body>
            <div class='container fade-in'>
                <div class='header'>🎉 Your Slot has booked! 🎉</div>
                <p>Dear {user.FullName},</p>
                <p>Thank you for your slot! Our team will arrive soon.</p>

                <div class='progress-bar'>
                    <div></div>
                </div>

                <h3>Booking Details</h3>
                <table class='order-summary'>
                    <tr>
                        <th>Item Name</th>
                        <th>Quantity</th>
                        <th>Price</th>
                        <th>Total</th>
                    </tr>
                    {orderItemsHtml}
                </table>

                <h3>Cleaning Team Details</h3>
                <p><b>Team Name:</b> {teamName}</p>
                <p><b>Assigned Employee:</b> {employeeName}</p>

                <h3>Your Address</h3>
                <p>{order.Address}</p>

                <div class='footer'>
                    <p>Need help? <a href='#'>Contact Support</a></p>
                    <p>Thank you for choosing us! 🚀</p>
                </div>
            </div>
        </body>
        </html>";

            // Send email
            await _emailService.SendEmailAsync(user.Email, "Order Confirmation - Your Cleaning Team is Ready!", userEmailBody);
        }


        public async Task<OrderMaster> GetOrderDetails(string userID)
        {
            try
            {
                int userIdInt = Convert.ToInt32(userID);

                var orderData = await (from order in _dbContext.Orders
                                       join detail in _dbContext.OrderDetails
                                       on order.Id equals detail.OrderId
                                       join subCategory in _dbContext.SubCategories
                                       on detail.SubCategoryId equals subCategory.SubCategoryId
                                       join category in _dbContext.Categories
                                       on subCategory.CategoryId equals category.CategoryId
                                       where order.UserId == userIdInt
                                       select new { order, detail, subCategory, category })
                                       .ToListAsync();

                if (!orderData.Any())
                {
                    return new OrderMaster(); 
                }

                var orderMain = orderData.First().order;

                var orderDetails = orderData
                    .GroupBy(x => x.detail.OrderId) 
                    .Select(g => new OrderDetails
                    {
                        OrderNumber = g.First().detail.OrderId,
                        OrderDate = g.First().order.OrderDate,
                        OrderAmount = g.Sum(x => x.order.TotalAmount),
                        CategoryName = g.First().category.CategoryName,
                        Quantity = g.Sum(x => x.detail.Quantity),
                        SubCategoryName = g.First().subCategory.SubCategoryName
                    })
                    .ToList();

                OrderMaster orderMaster = new OrderMaster
                {
                    OrderDetails = orderDetails
                };

                return orderMaster;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new OrderMaster();
            }
        }

        public async Task<OrderMaster> GetOrderDetailForBill(int orderId)
        {
            try
            {
                var orderData = await (from order in _dbContext.Orders
                                       join detail in _dbContext.OrderDetails
                                       on order.Id equals detail.OrderId
                                       join subCategory in _dbContext.SubCategories
                                       on detail.SubCategoryId equals subCategory.SubCategoryId
                                       join category in _dbContext.Categories
                                       on subCategory.CategoryId equals category.CategoryId
                                       where order.Id == orderId
                                       select new { order, detail, subCategory, category })
                             .ToListAsync();

                if (!orderData.Any())
                {
                    return new OrderMaster();
                }

                var orderMain = orderData.First().order;

                var orderDetails = orderData.Select(x => new OrderDetails
                {
                    OrderNumber = x.detail.OrderId,
                    OrderDate = x.order.OrderDate,
                    OrderAmount = x.detail.TotalCategoryAmount,  // Assuming TotalAmount is per order
                    CategoryName = x.category.CategoryName,
                    SubCategoryName = x.subCategory.SubCategoryName,
                    Quantity = x.detail.Quantity,
                }).ToList();

                OrderMaster orderMaster = new OrderMaster
                {
                    OrderDate = orderMain.OrderDate,
                    TotalOrderAmount = orderMain.TotalAmount,
                    OrderDetails = orderDetails
                };

                return orderMaster;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new OrderMaster();
            }
        }

    }
}
