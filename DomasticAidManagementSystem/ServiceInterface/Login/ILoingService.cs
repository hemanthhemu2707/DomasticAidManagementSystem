using DomasticAidManagementSystem.Domain.Entities;

namespace  DomasticAidManagementSystem
{
    public interface ILoingService
    {
        public Task<User> CheckLogin(User user);

        public Task<User> RegisterUser(User request);

        public Task<bool> VerifyEmailExist(string email);

        public Task<bool> ResetPassword(User request);

    }
}
