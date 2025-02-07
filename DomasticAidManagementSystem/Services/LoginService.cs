using DomasticAidManagementSystem.Domain.Entities;

namespace DomasticAidManagementSystem
{
    public class LoginService : ILoingService
    {
        private readonly ILoginRepo _login;

        public LoginService(ILoginRepo login)
        {
            _login = login;
        }

        public async Task<User> CheckLogin(User user)
        {
            var response= await _login.CheckLogin(user);
            return response;
        }

        public async Task<User> RegisterUser(User request)
        {
            var response = await _login.RegisterNewUser(request);
            return response;
        }

        public async Task<bool> ResetPassword(User request)
        {
            var response = await _login.ResetPassword(request);
            return response;
        }

        public async Task<bool> VerifyEmailExist(string email)
        {
            var response = await _login.VerifyEmailExist(email);
            return response;
        }
    }
}
