namespace  DomasticAidManagementSystem
{
    public interface ILoingService
    {
        public Task<LoginRequest> CheckLogin(LoginRequest request);

        public Task<LoginRequest> RegisterUser(LoginRequest request);

        public Task<bool> VerifyEmailExist(string email);

        public Task<bool> ResetPassword(LoginRequest request);

    }
}
