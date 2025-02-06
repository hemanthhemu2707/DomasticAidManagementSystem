using DomasticAidManagementSystem.Domain.Entities;
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



        public async Task<User> CheckLogin(User request)
        {
            try
            {
                var user = await _dbContext.Users
                    .FirstOrDefaultAsync(x =>
                        (x.FullName == request.FullName || x.Email == request.Email)
                        && x.PasswordHash == request.PasswordHash);

                if (user != null)
                {
                    return new User
                    {
                        UserId = user.UserId,
                        FullName = user.FullName,
                        Email = user.Email,
                        Status = 1,
                        StatusMessage = "Login Successful"
                    };
                }
                else
                {
                    return new User
                    {
                        Status = 0,
                        StatusMessage = "Invalid Credentials"
                    };
                }
            }
            catch(Exception ex)
            {
                return new User();
            }
        }






        public async Task<User> RegisterNewUser(User request)
        {
            User loginRequest = request as User;
            if (loginRequest == null)
            {
                loginRequest = new User();
            }
            try
            {
                var dbType1 = await _dbContext.Users
       .Where(x => x.FullName == request.FullName)
       .ToListAsync();  
            }
            catch(Exception ex)
            {

            }

            var dbType = await _dbContext.Users
       .Where(x => x.FullName == request.FullName)
       .ToListAsync();  // Using ToList() instead of ToListAsync()

        

                var existingUserByEmail = await _dbContext.Users
        .Where(x => x.Email == request.Email)
        .FirstOrDefaultAsync();

                var existingUserByFullNameUpperCase = await _dbContext.Users
        .Where(x => x.FullName.ToUpper() == request.FullName.ToUpper())
        .FirstOrDefaultAsync();


                if (existingUserByEmail != null)
                {
                    loginRequest.Status = 0;
                    loginRequest.StatusMessage = "Email Already Registerd ! Please Login ";

                }
                else if (existingUserByFullNameUpperCase != null)

                {
                    loginRequest.Status = 0;
                    loginRequest.StatusMessage = "User Name Already Registerd ! Please Use Diffrent ";

                }
                else
                {
                    //await _dbContext.Users.AddAsync(newLeaveRecord);
                    //await _dbContext.SaveChangesAsync();
                    //loginRequest.Status = 1;
                    //loginRequest.StatusMessage = "Registration Sucessfully  ? Request Sent to ADMIN wait for Approve Check Email !";

                  

                    string EmailBody = $"<p>Dear {request.FullName},</p><p>Your registration was successful. Please wait for admin approval.</p>";
                    await _emailService.SendEmailAsync(request.Email, "Registration Successful", EmailBody);

      //              var AdminEmail = await _dbContext.Users
      //.Where(x => x.UserApproveStatus == 1)
      //.FirstOrDefaultAsync();
      //              string adminEmailBody = $"<p>A new user, {request.FullName}, has registered and is awaiting approval.</p>";
      //              await _emailService.SendEmailAsync(AdminEmail.Email, "New User Registration", adminEmailBody);
                }



            return loginRequest;

        }


        public async Task<bool> VerifyEmailExist(string email)
        {
            var details = await _dbContext.Users.Where(x => x.Email == email).ToListAsync();
            return details.Any();
        }
    }

    }

