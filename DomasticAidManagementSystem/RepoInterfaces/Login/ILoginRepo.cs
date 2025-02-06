
using DomasticAidManagementSystem.Domain.Entities;

namespace DomasticAidManagementSystem
{
    public interface ILoginRepo
    {
        public Task<User> CheckLogin(User request);
        public Task<User> RegisterNewUser(User request);
        public Task<bool> VerifyEmailExist(string email);
    }
}
