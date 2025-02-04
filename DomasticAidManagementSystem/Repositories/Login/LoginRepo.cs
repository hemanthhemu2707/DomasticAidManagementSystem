using HEMANTH.HOME_EXPENSE.ServiceInterface;
using IIITS.EFCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DomasticAidManagementSystem
{
    public class LoginRepo : ILoginRepo
    {
        private readonly EmailService _emailService;

        private readonly IConfiguration _configuration;

        private readonly LMSMasterServiceDBContext _dbContext;

        public LoginRepo(LMSMasterServiceDBContext dbContext, IConfiguration configuration, EmailService emailService)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _emailService = emailService;
        }



        public async Task<LoginRequest> CheckLogin(LoginRequest request)
        {
            LoginRequest loginRequest = request as LoginRequest;
            if (loginRequest.RoleType == 1)
            {
                var dbType = await _dbContext.LoginDetails
                                    .Where(x => x.UserName == request.UserName && x.UserPassword == request.Password && x.AdminStatus == 1)
                                    .ToListAsync();
                if (dbType.Count > 0)
                {
                    loginRequest.Status = 1;
                    loginRequest.StatusMessage = "Login successful!";
                    loginRequest.RoleType = 1;

                }
                else
                {
                    loginRequest.Status = 0;
                    loginRequest.StatusMessage = "Invalid Admin Crediatials !";
                }
            }
            else
            {

                try
                {

                var dbType = await _dbContext.LoginDetails
                    .Where(x => x.UserName == request.UserName && x.UserPassword == request.Password)
                    .ToListAsync();
                // Ensure the result is awaited properly

                // Check if result is not null and contains a valid boolean status
                if (dbType.Count > 0)
                {
                    if (dbType.FirstOrDefault().UserApproveStatus == 0)
                    {
                        loginRequest.Status = 0;
                        loginRequest.StatusMessage = "Please Wait Untill Approve !";
                    }
                    else
                    {
                        loginRequest.Status = 1;
                        loginRequest.StatusMessage = "Login successful!";
                    }

                }
                else
                {
                    loginRequest.Status = 0;
                    loginRequest.StatusMessage = "Invalid credentials!";
                }
            }
                catch(Exception ex)
                    {

                }
            }
            // Return the login request after processing
            return loginRequest;
        }




        public async Task<LoginRequest> RegisterNewUser(LoginRequest request)
        {
            LoginRequest loginRequest = request as LoginRequest;
            if (loginRequest == null)
            {
                loginRequest = new LoginRequest();
            }
            try
            {
                var dbType1 = await _dbContext.LoginDetails
       .Where(x => x.UserName == request.UserName)
       .ToListAsync();  // Using ToList() instead of ToListAsync()
            }
            catch(Exception ex)
            {

            }

            var dbType = await _dbContext.LoginDetails
       .Where(x => x.UserName == request.UserName)
       .ToListAsync();  // Using ToList() instead of ToListAsync()

            if (dbType != null && dbType.Count > 0) 
            {

                loginRequest.UserId = dbType.FirstOrDefault().UserId;
                loginRequest.ApprovalStatus = dbType.FirstOrDefault().UserApproveStatus;

                if (loginRequest.ApprovalStatus == 0)
                {
                    loginRequest.Status = 0;
                    loginRequest.StatusMessage = "Email Already Registerd ! Please Wait for Approval ";
                }
                else
                {
                    loginRequest.Status = 0;
                    loginRequest.StatusMessage = "Email Already Registerd ! Please Login ";
                }

                       }
            else
            {
                var newLeaveRecord = new LoginDBTypes

                {
                    UserName = request.UserName,
                    UserPhone = request.UserPhoneNumber,
                    UserEmailAdress = request.UserEmail,
                    UserPassword = request.Password,
                    AdminStatus=0
                    
                };

                var existingUserByEmail = await _dbContext.LoginDetails
        .Where(x => x.UserEmailAdress == request.UserEmail)
        .FirstOrDefaultAsync();

                var existingUserByUserNameUpperCase = await _dbContext.LoginDetails
        .Where(x => x.UserName.ToUpper() == request.UserName.ToUpper())
        .FirstOrDefaultAsync();


                if (existingUserByEmail != null)
                {
                    loginRequest.Status = 0;
                    loginRequest.StatusMessage = "Email Already Registerd ! Please Login ";

                }
                else if (existingUserByUserNameUpperCase != null)

                {
                    loginRequest.Status = 0;
                    loginRequest.StatusMessage = "User Name Already Registerd ! Please Use Diffrent ";

                }
                else
                {
                    await _dbContext.LoginDetails.AddAsync(newLeaveRecord);
                    await _dbContext.SaveChangesAsync();
                    loginRequest.Status = 1;
                    loginRequest.StatusMessage = "Registration Sucessfully  ? Request Sent to ADMIN wait for Approve Check Email !";

                  

                    string userEmailBody = $"<p>Dear {request.UserName},</p><p>Your registration was successful. Please wait for admin approval.</p>";
                    await _emailService.SendEmailAsync(request.UserEmail, "Registration Successful", userEmailBody);

                    var AdminEmail = await _dbContext.LoginDetails
      .Where(x => x.UserApproveStatus == 1)
      .FirstOrDefaultAsync();
                    string adminEmailBody = $"<p>A new user, {request.UserName}, has registered and is awaiting approval.</p>";
                    await _emailService.SendEmailAsync(AdminEmail.UserEmailAdress, "New User Registration", adminEmailBody);
                }




            }

            return loginRequest;
        }

    }
}
