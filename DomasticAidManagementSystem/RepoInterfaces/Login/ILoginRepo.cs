
namespace DomasticAidManagementSystem
{
    public interface ILoginRepo
    {
        public Task<LoginRequest> CheckLogin(LoginRequest request);
        public Task<LoginRequest> RegisterNewUser(LoginRequest request);

    }
}
