namespace DomasticAidManagementSystem
{
    public class LoginService : ILoingService
    {
        private readonly ILoginRepo _login;

        public LoginService(ILoginRepo login)
        {
            _login = login;
        }

        public async Task<LoginRequest> CheckLogin(LoginRequest request)
        {
            LoginRequest loginRequest = new LoginRequest();
            loginRequest= await _login.CheckLogin(request);
            return  loginRequest;
        }

        public async Task<LoginRequest> RegisterUser(LoginRequest request)
        {
            LoginRequest loginRequest = new LoginRequest();
            loginRequest = await _login.RegisterNewUser(request);
            return loginRequest;
        }

        public Task<bool> ResetPassword(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<bool> VerifyEmailExist(string email)
        {
            throw new NotImplementedException();
        }
    }
}
