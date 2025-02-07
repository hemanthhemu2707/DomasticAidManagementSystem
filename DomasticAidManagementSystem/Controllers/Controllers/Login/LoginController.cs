using DomasticAidManagementSystem;
using DomasticAidManagementSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
namespace HEMANTH.HOME_EXPENCE.Controllers.Login
{
    public class LoginController : Controller
    {
        private readonly EmailService _emailService;

        private readonly ILoingService _loginService;

        public LoginController(ILoingService loginService, EmailService emailService)
        {
            _loginService = loginService;
            _emailService = emailService;

        }

        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Remove("SessionExpiredMessage");

            return View();
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            HttpContext.Session.Remove("SessionExpiredMessage");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var res = await _loginService.CheckLogin(user);
            if (res.Status == 1)
            {
                HttpContext.Session.SetString("UserId", res.UserId.ToString());
                HttpContext.Session.SetString("UserName", res.FullName.ToString());
                HttpContext.Session.SetString("IsAdmin", res.Role.ToString());
                res.EncrptedUserId = EncryptionHelper.UrlEncrypt(res.UserId.ToString());
            }
            return Json(res);
        }





        [HttpGet]
        public IActionResult RegisterUser()
        {
            User _login = new User();
            return View(_login);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] User _login)
        {

            var res = await _loginService.RegisterUser(_login);

            return Json(res);
        }



        public IActionResult ForgotPassword()
        {

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> GenerateOTP(string email)
        {
            var otp = new Random().Next(1000, 9999).ToString();
            var response = await _loginService.VerifyEmailExist(email);
            if (response)
            {

                return Json(new { success = false, Message = "Email Already Used Please Use Diffrent !" });

            }
            else
            {
    
                    var emailBody = @"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <style>
                                body {
                                    background-color: green;
                                    color: white;
                                    font-family: Arial, sans-serif;
                                    text-align: center;
                                    padding: 20px;
                                }
                                .otp {
                                    color: red;
                                    font-size: 24px;
                                    font-weight: bold;
                                }
                            </style>
                        </head>
                        <body>
                            <h1>Verification Code</h1>
                            <p>Your One-Time Password (OTP) is:</p>
                            <div class='otp'>" + otp + @"</div>
                            <p>Please use this code to complete your verification process.</p>
                        </body>
                        </html>";

                    bool statusM = await _emailService.SendEmailAsync(email, "Your OTP", emailBody);
                    if (statusM)
                    {
                        return Json(new { success = true, otp = otp });
                    }
       


                return Json(new { success = false, otp = otp });
            }
        }


        public async Task<IActionResult> ForgotGenerateOTP(string email)
        {
            var otp = new Random().Next(1000, 9999).ToString();
            var response = await _loginService.VerifyEmailExist(email);

                if (response)
                {
                    var emailBody = @"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <style>
                                body {
                                    background-color: green;
                                    color: white;
                                    font-family: Arial, sans-serif;
                                    text-align: center;
                                    padding: 20px;
                                }
                                .otp {
                                    color: red;
                                    font-size: 24px;
                                    font-weight: bold;
                                }
                            </style>
                        </head>
                        <body>
                            <h1>Verification Code</h1>
                            <p>Your One-Time Password (OTP) is:</p>
                            <div class='otp'>" + otp + @"</div>
                            <p>Please use this code to complete your verification process.</p>
                        </body>
                        </html>";

                    bool statusM = await _emailService.SendEmailAsync(email, "Your OTP", emailBody);
                    if (statusM)
                    {
                        return Json(new { success = true, otp = otp });
                    }
                }
                else
                {
                    return Json(new { success = false, otp = "Emial Not Found !" });

                }


                return Json(new { success = false, otp = otp });
            }





        



        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword([FromBody] User Details)
            {
            var res = await _loginService.ResetPassword(Details);
            return Json(new { success = res });
        }




    }
}
