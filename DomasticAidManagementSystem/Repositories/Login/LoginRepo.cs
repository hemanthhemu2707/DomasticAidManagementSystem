using DomasticAidManagementSystem.Domain.Entities;
using DomasticAidManagementSystem.Repositories.DBConfig.Login;
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
                UsersTableDBType user = new UsersTableDBType();
                if (request.Role == 1)
                {
                    user = await _dbContext.Users
                    .FirstOrDefaultAsync(x => x.Role==1 &&
                        (x.FullName == request.FullName || x.Email == request.FullName)
                        && x.PasswordHash == request.PasswordHash);
                }
                else
                {
                    user = await _dbContext.Users
                        .FirstOrDefaultAsync(x =>
                            (x.FullName == request.FullName || x.Email == request.FullName)
                            && x.PasswordHash == request.PasswordHash);
                }

                if (user != null)
                {
                    return new User
                    {
                        UserId = user.UserId,
                        FullName = user.FullName,
                        Email = user.Email,
                        Status = 1,
                        Role=user.Role,
                        Phone=user.Phone,
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
                return new User
                { 
                    Status = 0,
                        StatusMessage = "Something Went Wrong try agin!"
                    };
        }
        }

        public async Task<User> RegisterNewUser(User request)
        {
            try
            {
                var userDbType = new UsersTableDBType
                {
                    Email = request.Email,
                    Address = request.Address,
                    FullName = request.FullName,
                    PasswordHash = request.PasswordHash,
                    Phone = request.Phone,
                    Role = request.Role,
                };
                var userDetails=await _dbContext.AddAsync(userDbType);
                await _dbContext.SaveChangesAsync();
                if(userDetails!=null)
                {
                    string EmailBody = $"<p>Dear {request.FullName},</p><p>Your registration was successful. Please login.</p>";
                    await _emailService.SendEmailAsync(request.Email, "Registration Successful", EmailBody);
                    request.Status = 1;
                    request.StatusMessage = "User Registed Sucessfully Please Login";
                }
                else
                {
                    request.Status = 0;
                    request.StatusMessage = "Failed to Create User Try agin !";

                }
            }

            catch (Exception ex)
            {

            }

                   



            return request;

        }


        public async Task<bool> VerifyEmailExist(string email)
            {
            var details = await _dbContext.Users.Where(x => x.Email == email).ToListAsync();
            return details.Any();
        }

        public async Task<bool> ResetPassword(User request)
        {
            var details = await _dbContext.Users.Where(x => x.Email == request.Email).FirstOrDefaultAsync();
            if(details.UserId>0)
            {
                details.PasswordHash = request.PasswordHash;
            }
            await _dbContext.SaveChangesAsync();
            return details.UserId > 0;
        }
    }

    }

